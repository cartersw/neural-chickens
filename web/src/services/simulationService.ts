const API_URL = process.env.NEXT_PUBLIC_LOCAL_API_URL;

import { Console } from "console";
import { SimulationInfo } from "../app/types/simulation"

export async function getSimulationInfo(
        id: number
): Promise<SimulationInfo> {
    const response = await fetch(
        `${API_URL}/api/simulations/${id}`
    );

    console.log(`${API_URL}`);


    if(!response.ok) {
        const error = await response.json();
        
        throw new Error(
            error.message ?? "Failure"
        );
    }

    return response.json();
}