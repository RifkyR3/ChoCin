export class ApiBase {
	private authToken;

	protected constructor(token: string = 'the-authentication-token') {
		this.authToken = token;
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