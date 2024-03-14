
export async function httpGet<T>(request: string): Promise<T> {
    const response = await fetch(`/api/${request}`);

    if(!response.ok){
        throw new Error(response.statusText);
    }
    
    const body : T = await response.json();
    return body;
}

export async function httpPost<T>(request: string, entity: T){
    const response = await fetch(`/api/${request}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(entity)
    });

    const body : T = await response.json();

    if(!response.ok){
        throw new Error(response.statusText);
    }
    
    return body;
}

export function httpDelete(request: string) {
    fetch(`/api/${request}`, {
        method: 'DELETE'
    });
}

export function httpPut<T>(entityType: string, entityId: string, entity: T){
    fetch("/api/"+entityType+'/'+entityId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(entity)
    });
}