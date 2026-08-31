"use client";

import React from 'react';
import { useState } from "react";
import { getSimulationInfo } from '@/services/simulationService';
import { SimulationInfo } from "./types/simulation";


const Page = () => {
  const [simulation, setSimulation] = useState<SimulationInfo | null>(null);
  const [error, setError] = useState("");

  const handleGetSimulationInfo = async () => {
    try{
      setError("");

      const data = await getSimulationInfo(1);

      setSimulation(data);
    } catch (error){
      if(error instanceof Error){
        setError(error.message);
      }else {
        setError("Something went wrong");
      }
    }
  };

  return (
    <main className="min-h-screen flex items-center justify-center pt-18">
      <div className="flex flex-col items-start gap-4 w-[min(60vh,90vw)] p-5">
          <div className="w-full aspect-square rounded-lg border-2 border-foreground/15 bg-foreground/5" />

          <button
            onClick={handleGetSimulationInfo}
            className="bg-white border-2 text-black px-4 py-2 rounded-lg cursor-pointer"
          >Request Simulation</button>
          {simulation && (
            <div className ="text-black">
              <p>ID: {simulation.id}</p>
              <p>Status: {simulation.status}</p>
              <p>
                Created:{" "}
                {new Date(
                  simulation.createdAt
                ).toLocaleString()}
              </p>
              <p>
                Started:{" "}
                {new Date(
                  simulation.startedAt
                ).toLocaleString()}
              </p>
            </div>

          )}
          {error && (
            <p className="text-red-300">
            {error}
            </p>
          )}


        </div>

    </main>
  );


};

export default Page;