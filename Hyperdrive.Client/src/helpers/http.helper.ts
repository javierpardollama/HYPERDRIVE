import { HttpParams } from "@angular/common/http";

export class HttpHelper {
    public static toFormData(object: object): FormData {

        const formData = new FormData();

        Object.entries(object).forEach(property => {
            const [key, value] = property;
            if (Array.isArray(value)) {
                value.forEach(item => {
                    if (item instanceof File) {
                        let file = item as File;
                        formData.append(`${key}`, file, file.name);
                    } else if (item) {
                        formData.append(`${key}`, item);
                    }
                });
            } else {
                if (value instanceof File) {
                    let file = value as File;
                    formData.append(`${key}`, file, file.name);
                } else if (value) {
                    formData.append(`${key}`, value);
                }
            }
        });

        return formData;
    }

    public static toQueryParams(object: object): { params: HttpParams } {

        const options = {
            params: new HttpParams()
        };

        Object.entries(object).forEach(property => {

            const [key, value] = property;

            if (Array.isArray(value)) {
                value.forEach(item => {
                    options.params = options.params.append(`${key}`, item)
                });

            } else {
                options.params = options.params.append(`${key}`, value)
            }
        });

        return options;
    }
}