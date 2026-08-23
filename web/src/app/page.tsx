"use client";

import React from 'react';
import { useState } from "react";
import { getSimulationInfo } from "./services/simulationService";
import { SimulationInfo } from './types/simulation';


const Page = () => {
  const [simulation, setSimulation] = useState<SimulationInfo | null>(null);
  const [error, setError] = useState("");

  const handleGetSimulation = async () => {
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
            <div className="flex justify-center gap-8 w-[90%] p-5">

              
            </div>
        </main>
  );


};

export default Page;