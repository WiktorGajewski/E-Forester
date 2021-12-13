import { AuthService } from "../auth/auth.service";

export function appInitializer(auth: AuthService): () => Promise<void> {
    return () => new Promise<void>((resolve, reject) => {
        auth.refreshToken()
                .subscribe()
                .add(resolve);
    });
}