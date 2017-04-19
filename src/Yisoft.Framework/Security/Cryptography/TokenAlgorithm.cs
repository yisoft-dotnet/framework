//      )                             *     
//   ( /(        *   )       (      (  `    
//   )\()) (   ` )  /( (     )\     )\))(   
//  ((_)\  )\   ( )(_)))\ ((((_)(  ((_)()\  
// __ ((_)((_) (_(_())((_) )\ _ )\ (_()((_) 
// \ \ / / (_) |_   _|| __|(_)_\(_)|  \/  | 
//  \ V /  | | _ | |  | _|  / _ \  | |\/| | 
//   |_|   |_|(_)|_|  |___|/_/ \_\ |_|  |_| 
// 
// This file is subject to the terms and conditions defined in
// file 'License.txt', which is part of this source code package.
// 
// Copyright © Yi.TEAM. All rights reserved.
// -------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Yisoft.Framework.Extensions;

namespace Yisoft.Framework.Security.Cryptography
{
	public class TokenAlgorithm
	{
		private const int _TIMESTAMP_LENGTH = 8;

		private const string _RANDOM_SEEDS = "0123456789abcdef";

		private static readonly Random _Random = new Random();

		private readonly ITokenEncoder _backpack;
		private readonly TokenHashSettings _settings;

		public TokenAlgorithm(TokenHashSettings settings = null) : this(settings, new BackpackEncoder()) { }

		public TokenAlgorithm(TokenHashSettings settings, ITokenEncoder backpack)
		{
			_settings = settings ?? TokenHashSettings.SHA256;
			_backpack = backpack ?? throw new ArgumentNullException(nameof(backpack));
		}

		public string Encrypt(string input, string key, string data = null)
		{
			var versionPosition = _Random.Next(_settings.VersionPosStart, _settings.VersionPosEnd - _settings.VersionLength);
			var saltPosition = _Random.Next(_settings.SaltPosStart, _settings.SaltPosEnd - _settings.SaltLength);
			var dataPosition = _Random.Next(_settings.DataPosStart, _settings.DataPosEnd - _settings.DataMaxlength);
			var timestamp = DateTime.Now.ToTimestamp();
			var salt = new TokenSalt(_GenerateSalt(), data, timestamp, versionPosition, saltPosition, dataPosition, data?.Length ?? 0);

			return Encrypt(input, key, salt);
		}

		public string Encrypt(string input, string key, TokenSalt salt)
		{
			var keyBuffer = Encoding.UTF8.GetBytes(key + salt.Salt);

			using (var sha = IncrementalHash.CreateHMAC(_settings.HashAlgorithm, keyBuffer))
			{
				sha.AppendData(Encoding.UTF8.GetBytes(input));

				var buffer = sha.GetHashAndReset();
				var token = BitConverter.ToString(buffer).Replace("-", string.Empty).ToLower();

				return _AddSalt(token, salt);
			}
		}

		public TokenSalt GetSalt(string token)
		{
			var decoded = _backpack.Decode(token);
			var versionPosition = Convert.ToInt32(decoded[2].ToString());
			var saltPosition = Convert.ToInt32($"{decoded[versionPosition + 1]}{decoded[versionPosition + 2]}");
			var dataPosition = Convert.ToInt32($"{decoded[versionPosition + 3]}{decoded[versionPosition + 4]}");
			var dataLength = Convert.ToInt32($"{decoded[versionPosition + 5]}{decoded[versionPosition + 6]}");
			var salt = decoded.Substring(saltPosition, _settings.SaltLength);
			var data = decoded.Substring(dataPosition, dataLength);
			var timestamp = decoded.Substring(dataPosition + _settings.DataMaxlength - _TIMESTAMP_LENGTH, _TIMESTAMP_LENGTH);

			return new TokenSalt(salt, data, int.Parse(timestamp, NumberStyles.HexNumber), versionPosition, saltPosition, dataPosition, dataLength);
		}

		protected virtual string _GenerateSalt()
		{
			var salt = new StringBuilder();

			while (salt.Length < _settings.SaltLength)
			{
				var index = _Random.Next(_RANDOM_SEEDS.Length);

				salt.Append(_RANDOM_SEEDS[index]);
			}

			return salt.ToString();
		}

		protected virtual string _AddSalt(string token, TokenSalt salt)
		{
			token = _Replace(token, 2, salt.VersionPosition.ToString());

			// add version
			token = _SetVersion(token, salt.VersionPosition);

			// add salt
			token = _Replace(token, salt.VersionPosition + 1, salt.SaltPosition.ToString("D2"));
			token = _Replace(token, salt.SaltPosition, salt.Salt);

			// add data and timestamp
			token = _Replace(token, salt.VersionPosition + 5, salt.DataLength.ToString("D2"));
			token = _Replace(token, salt.VersionPosition + 3, salt.DataPosition.ToString("D2"));

			if (!string.IsNullOrEmpty(salt.Data))
			{
				if (salt.Data.Length > _settings.DataMaxlength - _TIMESTAMP_LENGTH)
				{
					var message = $"data length is not in range, max length is {_settings.DataMaxlength - _TIMESTAMP_LENGTH}";

					throw new ArgumentOutOfRangeException(nameof(salt.Data), message);
				}

				token = _Replace(token, salt.DataPosition, salt.Data);
			}

			token = _Replace(token, salt.DataPosition + _settings.DataMaxlength - _TIMESTAMP_LENGTH, salt.Timestamp.ToString("x8"));

			// apply backpack
			return _backpack.Encode(token);
		}

		private string _SetVersion(string token, int position) { return _Replace(token, position, _settings.Version.ToString()); }

		private static string _Replace(string input, int position, string value)
		{
			input = input.Remove(position, value.Length);

			return input.Insert(position, value);
		}
	}
}
