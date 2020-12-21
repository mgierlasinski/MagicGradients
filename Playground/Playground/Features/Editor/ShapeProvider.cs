﻿using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Playground.Features.Editor
{
    public interface IShapeProvider
    {
        Task<ShapeSnippet> ShowActionSheet();
    }

    public class ShapeProvider : IShapeProvider
    {
        private readonly ShapeSnippet[] _shapes = 
        {
            new ShapeSnippet
            {
                Name = "Star",
                Data = "M 0 -100 L 58.8 90.9, -95.1 -30.9, 95.1 -30.9, -58.8 80.9 Z"
            },
            new ShapeSnippet
            {
                Name = "Xamagon",
                Data = "M73.866 0c-6.914.015-13.682 3.94-17.162 9.927L2.57 103.963c-3.427 6.003-3.427 13.85 0 19.853l54.134 94.037c3.48 5.987 10.248 9.913 17.162 9.927h108.268c6.914-.015 13.682-3.94 17.162-9.927l54.134-94.037c3.427-6.003 3.426-13.85 0-19.853L199.296 9.927C195.816 3.939 189.048.014 182.134 0H73.866zm.983 55.013c.149-.015.305-.015.454 0h18.674a2.462 2.462 0 0 1 2.042 1.212l31.679 56.452c.16.28.262.59.3.91.04-.32.142-.63.302-.91l31.603-56.452a2.47 2.47 0 0 1 2.117-1.212h18.675c1.653.014 2.892 2.097 2.117 3.561l-30.923 55.316 30.923 55.24c.848 1.472-.42 3.651-2.117 3.637H162.02a2.47 2.47 0 0 1-2.117-1.288L128.3 115.026c-.16-.279-.262-.59-.301-.909a2.43 2.43 0 0 1-.301.91l-31.68 56.452a2.468 2.468 0 0 1-2.04 1.288H75.302c-1.697.015-2.965-2.165-2.117-3.637l30.923-55.24-30.923-55.316c-.741-1.336.163-3.276 1.663-3.561z"
            }
        };

        public async Task<ShapeSnippet> ShowActionSheet()
        {
            var values = _shapes.Select(x => x.Name).ToArray();
            var result = await Shell.Current.DisplayActionSheet("Pick shape", "Cancel", null, values);
            var selection = _shapes.FirstOrDefault(x => x.Name == result);

            return selection;
        }
    }

    public class ShapeSnippet
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }
}
