using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.Interfaces
{
    public interface IVideoStorage
    {
        Task<string> SaveAsync(Stream video, string fileName, CancellationToken cancellationToken);
        Task<Stream> GetAsync(string videoKey, CancellationToken cancellationToken);
        Task DeleteAsync(string videoKey, CancellationToken cancellationToken);
    }
}
