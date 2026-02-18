import { AbstractControl, ValidationErrors, FormArray } from "@angular/forms";
import { IsEmpty } from "src/utils/object.utils";

export class CustomValidators {

    public static Numeric(control: AbstractControl): ValidationErrors | null {
        if (IsEmpty(control.value)) return null;

        return isNaN(Number(control.value)) ? { notnumeric: true } : null;
    }

    public static Duplicated(fields: string[]): ValidationErrors | null {
        return (control: AbstractControl): ValidationErrors | null => {
            const formarray = control as FormArray;
            const groups = formarray.controls;

            // Build a unique key based on fields
            const controlkey = (group: AbstractControl): string =>
                fields
                    .map(f => group.get(f)?.value ?? '')
                    .join('|');

            // Clean duplicate errors without touching other errors
            const cleanduplicatederrors = (group: AbstractControl): void => {
                const current = group.errors;
                if (!current) return;

                const { duplicate, ...rest } = current;
                group.setErrors(Object.keys(rest).length > 0 ? rest : null);
            };

            // First step: Group rows by unique key
            const indexmap = new Map<string, number[]>();

            groups.forEach((group, index) => {
                const key = controlkey(group);
                const list = indexmap.get(key) ?? [];
                list.push(index);
                indexmap.set(key, list);
            });

            // Clean old duplicate errors
            groups.forEach(cleanduplicatederrors);

            let hasduplicates = false;

            // Second step: Attach new duplicate errors
            for (const indexes of indexmap.values()) {
                if (indexes.length <= 1) continue;

                hasduplicates = true;

                indexes.forEach(i => {
                    const group = groups[i];
                    group.setErrors({ ...(group.errors ?? {}), duplicate: true });
                });
            }

            return hasduplicates ? { duplicated: true } : null;
        };
    }

    public static Empty(): ValidationErrors | null {
        return (control: AbstractControl): ValidationErrors | null => {
            const v = control.value;

            const isempty = IsEmpty(v);

            return isempty ? { empty: true } : null;
        };
    }
}