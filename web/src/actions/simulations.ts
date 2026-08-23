"use server";

import { getSimulationInfo } from "@/services/simulationService";

export async function fetchSimulationInfo(id: number){
    return getSimulationInfo(id);
}