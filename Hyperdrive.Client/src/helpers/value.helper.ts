export class ValueHelper {

	public static IsDefined<T>(value: T): value is T {
		return <T>value !== undefined && <T>value !== null;
	}

	public static IsEmpty<T>(value: T): boolean {
		if (!this.IsDefined(value)) return true;

		if (typeof value === 'string') {
			return value.trim().length === 0;
		}

		if (typeof value === 'object') {
			return Object.keys(value!).length === 0;
		}

		if (Array.isArray(value)) {
			return value.length === 0;
		}

		return false;
	}
}