using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Playground.Controls
{
    public partial class ActionMenu : Grid
    {
        private const double OffsetY = 40;
        private readonly SemaphoreSlim _semaphore;

        public static readonly BindableProperty IsOpenedProperty = BindableProperty.Create(
            nameof(IsOpened), typeof(bool), typeof(ActionMenu), false, BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) => (bindable as ActionMenu)?.OnOpenedChanged());

        public static readonly BindableProperty ActionsProperty = BindableProperty.Create(
            nameof(Actions), typeof(ActionItemCollection), typeof(ActionMenu), new ActionItemCollection(), 
            propertyChanged: (bindable, oldValue, newValue) => (bindable as ActionMenu)?.OnActionsSourceChanged());

        public bool IsOpened
        {
            get => (bool)GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, value);
        }

        public ActionItemCollection Actions
        {
            get => (ActionItemCollection)GetValue(ActionsProperty);
            set => SetValue(ActionsProperty, value);
        }

        public ActionMenu()
        {
            InitializeComponent();

            _semaphore = new SemaphoreSlim(1, 1);
            
            BackgroundLayer.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => IsOpened = !IsOpened)
            });
        }

        private async Task OnOpenedChanged()
        {
            await _semaphore.WaitAsync();

            if (IsOpened)
            {
                SetupBeforeAnimation();
                await AnimateShowMenu();
            }
            else
            {
                await AnimateHideMenu();
            }
            _semaphore.Release(1);
        }

        private void OnActionsSourceChanged()
        {
            if (Actions != null)
            {
                BindableLayout.SetItemsSource(ActionsContainer, Actions.Where(x => x.IsVisible));
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            foreach (var item in Actions)
            {
                SetInheritedBindingContext(item, BindingContext);
            }
        }

        private void SetupBeforeAnimation()
        {
            Opacity = 1;
            BackgroundLayer.Opacity = 0;

            var multiply = ActionsContainer.Children.Count - 1;

            foreach (var actionItem in ActionsContainer.Children)
            {
                actionItem.Opacity = 0;
                actionItem.TranslationY = OffsetY + OffsetY * multiply--;
            }

            IsVisible = true;
        }

        private async Task AnimateShowMenu()
        {
#pragma warning disable 4014
            BackgroundLayer.FadeTo(0.8, 100);
#pragma warning restore 4014

            foreach (var child in GetActionItemsReordered(ActionsContainer))
            {
                var animation = new Animation();

                animation.WithConcurrent(f => child.Opacity = f, 0, 1);
                animation.WithConcurrent(f => child.TranslationY = f, child.TranslationY, 0, Easing.SinOut);

                child.Animate(nameof(AnimateShowMenu), animation, length: 150);
            }

            await Task.Delay(150);
        }

        private async Task AnimateHideMenu()
        {
            await this.FadeTo(0, 100);
            IsVisible = false;
        }

        private List<View> GetActionItemsReordered(Layout<View> container)
        {
            var actionItems = new List<View>();

            for (var i = container.Children.Count - 1; i >= 0; i--)
            {
                actionItems.Add(container.Children[i]);
            }

            return actionItems;
        }
    }
}