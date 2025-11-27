export function IsDefined<T>(value: T): value is T {
    return <T>value !== undefined && <T>value !== null;
}

export function IsEmpty<T>(value: T): boolean {
    if (!IsDefined(value)) return true;

    if (typeof value === 'string') return value.trim().length === 0;

    if (Array.isArray(value)) return value.length === 0;

    if (typeof value === 'object') {

        // Exclude special objects like Date, Map, Set
        if (value instanceof Date) return false;
        if (value instanceof Map || value instanceof Set) return value.size === 0;

        // For plain objects
        return Object.keys(value!).length === 0;
    }

    // For numbers, booleans, etc.
    return false;
}