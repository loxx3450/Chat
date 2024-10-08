﻿namespace CommonLibrary
{
    public enum MessageType
    {
        RegistrationRequest,
        RegistrationResponse,

        SigningInRequest,
        SigningInResponse,

        SessionStateCheckRequest,
        SessionStateCheckResponse,

        ResetPasswordRequest,
        ResetPasswordResponse,

        VerifyCodeForResetPasswordRequest,
        VerifyCodeForResetPasswordResponse,

        ChangePasswordRequest,
        ChangePasswordResponse,

        EmailVerificationRequest,
        EmailVerificationResponse,

        GettingDialoguesCardsRequest,
        GettingDialoguesCardsResponse,

        MessagesUploadRequest,
        MessagesUploadResponse
    }
}
