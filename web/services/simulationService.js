const API_URL = process.env.NEXT_LOCAL_API_URL;

export async function getSimulationInfo(id){
    const response = await fetch(
        '${API_URL}/simulations/${id}'
    );

    if(!response.ok){
        const error = await response.json();

        throw new Error(error.message ?? "Failure");
    }

    return await response.json();
}