﻿namespace PlaygroundMaui.Infrastructure
{
    public interface INavigationAware<TParameter>
    {
        void Prepare(TParameter parameter);
    }
}
