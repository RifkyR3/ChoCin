import { useTokenStore } from "@/stores";

export class ApiBase {
    private authToken: string = '';
    private setAuthTokenCall: boolean = false;

    public setAuthToken(token: string) {
        this.setAuthTokenCall = true;
        this.authToken = token;
    }

    protected transformOptions = (options: RequestInit): Promise<RequestInit> => {
        if (!this.setAuthTokenCall) {
            this.authToken = useTokenStore().token;
        }

        if (this.authToken != '') {
            options.headers = {
                ...options.headers,
                Authorization: this.authToken,
            };
        }
        return Promise.resolve(options);
    };
}