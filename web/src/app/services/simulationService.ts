const API_URL = process.env.NEXT_LOCAL_API_URL;

import { SimulationInfo } from "../types/simulation"

export async function getSimulationInfo(
        id: number
): Promise<SimulationInfo> {
    const response = await fetch(
        '${API_URL}/simulations/${id}'
    );

    if(!response.ok) {
        const error = await response.json();
        
        throw new Error(
            error.message ?? "Failure"
        );
    }

    return response.json();
}