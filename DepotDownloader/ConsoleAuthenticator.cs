// This file is subject to the terms and conditions defined
// in file 'LICENSE', which is part of this source code package.

using System;
using System.Threading.Tasks;
using SteamKit2.Authentication;

namespace DepotDownloader
{
    // This is practically copied from https://github.com/SteamRE/SteamKit/blob/master/SteamKit2/SteamKit2/Steam/Authentication/UserConsoleAuthenticator.cs
    internal class ConsoleAuthenticator : IAuthenticator
    {
        /// <inheritdoc />
        public Task<string> GetDeviceCodeAsync(bool previousCodeWasIncorrect)
        {
            if (previousCodeWasIncorrect)
            {
                Console.Out.WriteLine("[Info]|[Wrong2FA]|The previous 2-factor auth code you have provided is incorrect.");
            }

            string code;

            do
            {
                Console.Out.Write("[Info]|[2FA]|Please enter your 2 factor auth code from your authenticator app: ");
                code = Console.ReadLine()?.Trim();

                if (code == null)
                {
                    break;
                }
            }
            while (string.IsNullOrEmpty(code));

            return Task.FromResult(code!);
        }

        /// <inheritdoc />
        public Task<string> GetEmailCodeAsync(string email, bool previousCodeWasIncorrect)
        {
            if (previousCodeWasIncorrect)
            {
                Console.Out.WriteLine("[Info]|[WrongGuard]|The previous 2-factor auth code you have provided is incorrect.");
            }

            string code;

            do
            {
                Console.Out.Write($"[Info]|[Guard]|Please enter the authentication code sent to your email address: ");
                code = Console.ReadLine()?.Trim();

                if (code == null)
                {
                    break;
                }
            }
            while (string.IsNullOrEmpty(code));

            return Task.FromResult(code!);
        }

        /// <inheritdoc />
        public Task<bool> AcceptDeviceConfirmationAsync()
        {
            if (ContentDownloader.Config.SkipAppConfirmation)
            {
                return Task.FromResult(false);
            }

            Console.Out.WriteLine("[Info]|[MobileApp]|Use the Steam Mobile App to confirm your sign in...");

            return Task.FromResult(true);
        }
    }
}
