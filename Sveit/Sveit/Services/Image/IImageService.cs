using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.Services.Image
{
    public interface IImageService
    {
        Task<string> PostImage(string filename, string filepath);
    }
}
