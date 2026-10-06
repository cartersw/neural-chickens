using NeuralChickens.Api.Application.DTOs.ChickenBrain;
using NeuralChickens.Api.Application.Interfaces;
using NeuralChickens.Api.Common.Enums;
using NeuralChickens.Api.Common.Results;
using NeuralChickens.Api.Domain;
using NeuralChickens.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralChickens.Api.Application.Services
{
    public class ChickenBrainService(NeuralChickensDbContext context) : IChickenBrainService
    {
        public async Task<Result> CreateChickenBrainAsync(PostChickenBrainDto postChickenBrainDto)
        {
            var brainProcessingJob = new BrainProcessingJob
            {
                JobStatus = JobStatus.Requested,
                ChickenId = postChickenBrainDto.ChickenId,
                SimulationType = postChickenBrainDto.SimulationType
            };

            var chickenBrain = new ChickenBrain
            {
                SimulationType = postChickenBrainDto.SimulationType,
                CreatedAt = DateTime.UtcNow,
                ChickenId = postChickenBrainDto.ChickenId,
                BrainProcessingJob = brainProcessingJob
            };

            context.ChickenBrains.Add(chickenBrain);

            await context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
