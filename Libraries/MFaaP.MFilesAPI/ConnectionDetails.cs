﻿using System;
using System.Globalization;
using MFilesAPI;
using MFaaP.MFilesAPI.ExtensionMethods;

namespace MFaaP.MFilesAPI
{
	public class ConnectionDetails
	{
		/// <summary>
		/// The local timezone information, if available.
		/// </summary>
		public TimeZoneInformation TimeZoneInformation { get; set; }

		/// <summary>
		/// The authentication details to use to connect.
		/// </summary>
		/// <remarks>Defaults to the current logged-in Windows user.</remarks>
		public AuthenticationDetails AuthenticationDetails { get; set; }
			= AuthenticationDetails.CreateForLoggedOnWindowsUser();
		
		/// <summary>
		/// The server details to use to connect.
		/// </summary>
		/// <remarks>Defaults to the a TCP/IP connection to localhost on port 2266.</remarks>
		public ServerDetails ServerDetails { get; set; }
			= ServerDetails.CreateForTcpIp();

		/// <summary>
		/// The local computer name as reported for the server.  If left empty, will
		/// show the computer name provided by the system.
		/// </summary>
		public string LocalComputerName { get; set; }

		/// <summary>
		/// If this parameter is true, an anonymous connection can be created.
		/// </summary>
		public bool AllowAnonymousConnection { get; set; }
			= false;

		/// <summary>
		/// The culture information to use.
		/// </summary>
		public CultureInfo ClientCulture { get; set; }
			= CultureInfo.CurrentUICulture;

		/// <summary>
		/// Connects to a vault on the server.
		/// </summary>
		/// <param name="vaultGuid">The Guid of the vault to connect to.</param>
		/// <param name="vault">The connected vault, or null if connection failed.</param>
		/// <param name="serverApplication">The server application</param>
		/// <remarks>Exceptions during connection will be thrown.</remarks>
		public void ConnectToVault(Guid vaultGuid, out Vault vault, out MFilesServerApplication serverApplication)
		{
			serverApplication = null;
			vault = null;
			serverApplication = new MFilesServerApplication();

			// Attempt to connect to the vault using the extension method.
			if (serverApplication.Connect(this) == MFServerConnection.MFServerConnectionAuthenticated)
			{
				// Attempt to log into the vault.
				vault = serverApplication.LogInToVault(vaultGuid.ToString("B"));
			}
		}

		/// <summary>
		/// Tries to connect to a vault on the server.
		/// </summary>
		/// <param name="vaultGuid">The Guid of the vault to connect to.</param>
		/// <param name="vault">The connected vault, or null if connection failed.</param>
		/// <param name="serverApplication">The server application, or null if connection failed.</param>
		/// <returns>true if the connection happened without exceptions, false otherwise.</returns>
		public bool TryConnectToVault(Guid vaultGuid, out Vault vault, out MFilesServerApplication serverApplication)
		{
			try
			{
				this.ConnectToVault(vaultGuid, out vault, out serverApplication);
				return true;
			}
			catch
			{
				// HACK: Exception handling.
				vault = null;
				serverApplication = null;
				return false;
			}
		}

		/// <summary>
		/// Connects to a vault on the server in administrative mode.
		/// </summary>
		/// <param name="vaultGuid">The Guid of the vault to connect to.</param>
		/// <param name="vault">The connected vault, or null if connection failed.</param>
		/// <param name="serverApplication">The server application</param>
		/// <remarks>Exceptions during connection will be thrown.</remarks>
		public void ConnectToVaultAdministrative(
			Guid vaultGuid,
			out Vault vault,
			out MFilesServerApplication serverApplication)
		{
			serverApplication = null;
			vault = null;

			// Attempt to connect to the vault using the extension method.
			serverApplication = new MFilesServerApplication();
			if (serverApplication.Connect(this) == MFServerConnection.MFServerConnectionAuthenticated)
			{
				// Attempt to log into the vault.
				vault = serverApplication.LogInToVaultAdministrative(vaultGuid.ToString("B"));
			}
		}

		/// <summary>
		/// Tries to connect to a vault on the server in administrative mode.
		/// </summary>
		/// <param name="vaultGuid">The Guid of the vault to connect to.</param>
		/// <param name="vault">The connected vault, or null if connection failed.</param>
		/// <param name="serverApplication">The server application, or null if connection failed.</param>
		/// <returns>true if the connection happened without exceptions, false otherwise.</returns>
		public bool TryConnectToVaultAdministrative(
			Guid vaultGuid,
			out Vault vault,
			out MFilesServerApplication serverApplication)
		{
			try
			{
				this.ConnectToVaultAdministrative(vaultGuid, out vault, out serverApplication);
				return true;
			}
			catch
			{
				// HACK: Exception handling.
				vault = null;
				serverApplication = null;
				return false;
			}

		}
	}

}