using NeuralChickens.Api.Application.DTOs.ChickenBrain;
using NeuralChickens.Api.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.Interfaces
{
    public interface IChickenBrainService
    {
        Task<Result> CreateChickenBrainAsync(PostChickenBrainDto postChickenBrainDto);

    }
}
