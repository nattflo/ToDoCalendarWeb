
export async function httpGet<T>(request: string): Promise<T> {
    const response = await fetch(request);
    const body : T = await response.json();

    if(!response.ok){
        throw new Error(response.statusText);
    }
    
    return body;
}