function IsDefined<T>(value: T): value is T {
    return <T>value !== undefined && <T>value !== null;
}

function IsPlainObject(value: unknown): value is Record<string, unknown> {
    return typeof value === 'object' && Object.getPrototypeOf(value) === Object.prototype;
}

function IsCustomClass(value: unknown): boolean {
    return typeof value === 'object' && !IsPlainObject(value);
}

export function IsEmpty<T>(value: T): boolean {
    if (!IsDefined(value)) return true;

    if (typeof value === 'string') {
        return value.trim().length === 0;
    }

    if (Array.isArray(value)) {
        return value.length === 0 || value.every(v => IsEmpty(v));
    }

    if (value instanceof Map || value instanceof Set) {
        return value.size === 0;
    }

    if (value instanceof Date) {
        return false;
    }

    if (IsPlainObject(value)) {
        const keys = Object.keys(value);
        return keys.length === 0 || keys.every(key => IsEmpty(value[key]));
    }

    if (IsCustomClass(value)) {
        return false; // Custom classes as non-empty by default
    }

    return false; // Numbers, booleans, etc.
}