import { AuthService } from "../services/auth/auth.service";

export function appInitializer(authService: AuthService): () => Promise<void> {
    return () => new Promise<void>((resolve, reject) => {
        authService.refreshToken()
                .subscribe()
                .add(resolve);
    });
}