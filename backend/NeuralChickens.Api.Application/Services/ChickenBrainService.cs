using NeuralChickens.Api.Application.DTOs.ChickenBrain;
using NeuralChickens.Api.Application.Interfaces;
using NeuralChickens.Api.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.Services
{
    public class ChickenBrainService : IChickenBrainService
    {
        public Task<Result> CreateChickenBrainAsync(PostChickenBrainDto postChickenBrainDto)
        {
            throw new NotImplementedException();
        }
    }
}
