export class ValueHelper {
	public static IsNullOrEmpty(value: any): boolean {
		return value === null || value === undefined || value === "" || value ==='';
	}
}