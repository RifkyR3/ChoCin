import { useTokenStore } from "@/stores";

export class ApiBase {
	private authToken: string = '';

	protected constructor() {
		this.authToken = useTokenStore().token;
	}

	public setAuthToken(token: string) {
		this.authToken = token;
	}

	protected transformOptions = (options: RequestInit): Promise<RequestInit> => {
		if (this.authToken != '') {
			options.headers = {
				...options.headers,
				Authorization: this.authToken,
			};
		}
		return Promise.resolve(options);
	};
}