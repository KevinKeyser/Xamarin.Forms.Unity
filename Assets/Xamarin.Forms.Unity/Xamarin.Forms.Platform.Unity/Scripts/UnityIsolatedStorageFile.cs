using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Unity
{
	internal class UnityIsolatedStorageFile : IIsolatedStorageFile
	{
		readonly IsolatedStorageFile isolatedStorageFile;

		public UnityIsolatedStorageFile(IsolatedStorageFile isolatedStorageFile)
		{
			this.isolatedStorageFile = isolatedStorageFile;
		}

		public Task CreateDirectoryAsync(string path)
		{
			isolatedStorageFile.CreateDirectory(path);
			return Task.FromResult(true);
		}

		public Task<bool> GetDirectoryExistsAsync(string path)
		{
			return Task.FromResult(isolatedStorageFile.DirectoryExists(path));
		}

		public Task<bool> GetFileExistsAsync(string path)
		{
			return Task.FromResult(isolatedStorageFile.FileExists(path));
		}

		public Task<DateTimeOffset> GetLastWriteTimeAsync(string path)
		{
			return Task.FromResult(isolatedStorageFile.GetLastWriteTime(path));
		}
		
		public Task<Stream> OpenFileAsync(string path, FileMode mode, FileAccess access)
		{
			Stream stream = isolatedStorageFile.OpenFile(path, mode, access);
			return Task.FromResult(stream);
		}

		public Task<Stream> OpenFileAsync(string path, FileMode mode, FileAccess access, FileShare share)
		{
			Stream stream = isolatedStorageFile.OpenFile(path, mode, access, share);
			return Task.FromResult(stream);
		}
	}
}