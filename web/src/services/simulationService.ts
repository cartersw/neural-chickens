const API_URL = process.env.NEXT_PUBLIC_LOCAL_API_URL;

import { Console } from "console";
import { SimulationInfo } from "../app/types/simulation"

export async function getSimulationInfo(
        id: number
): Promise<SimulationInfo> {
    try{
        const response = await fetch(
            `${API_URL}/api/simulations/${id}`
        );

        if(!response.ok) {
            const contentType = response.headers.get("content-type");

            if(contentType?.includes("application/json")){
                const error = await response.json();

                throw new Error(
                    error.message ?? "Request failed."
                );
            }
            throw new Error(
                `Request failed with status ${response.status}.`
            );
            
        }
        return await response.json();
    } catch(error){
        if(error instanceof Error){
            throw error;
        }
        throw new Error("Something went wrong");
    }
    
}