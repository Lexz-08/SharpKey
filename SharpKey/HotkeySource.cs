using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SharpKey
{
	public enum FSModifiers
	{
		Alt     = 0x0001,
		Control = 0x0002,
		Shift   = 0x0004,
	}

	public class Hotkey
	{
		public const uint HOTKEY = 0x0312;

		private IntPtr windowHook;
		private int id;
		private uint fsModifier;
		private uint key;
		private bool enabled = false;
		private FSModifiers passedFSModifier;
		private Keys passedKey;
		private bool removed = false;

		public IntPtr WindowHook => windowHook;
		public int Id => id;
		public IntPtr Id_Ptr => (IntPtr)id;
		public uint FSModifier => fsModifier;
		public uint Key => key;
		public bool Enabled => enabled;

		public Hotkey(IntPtr winHook, int id, FSModifiers modifier, Keys key)
		{
			windowHook = winHook;
			this.id = id;
			fsModifier = (uint)modifier;
			this.key = (uint)key;

			passedFSModifier = modifier;
			passedKey = key;
		}

		[DllImport("user32")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		public void Hook(bool notify = true)
		{
			if (!removed)
			{
				if (!enabled)
				{
					if (RegisterHotKey(windowHook, id, fsModifier, key) && !enabled)
					{
						if (notify)
							MessageBox.Show($"Successfully hooked hotkey to:\n\n" +
								$"WinHook: {windowHook}\n" +
								$"Id: {id}\n" +
								$"FSModifier: {passedFSModifier}\n" +
								$"Key: {passedKey}\n\n" +
								$"You can use Hook() and Unhook() to toggle it, and Remove() to remove it.",
								"Hotkey Successfully Created and Hooked", MessageBoxButtons.OK, MessageBoxIcon.Information);

						enabled = true;
					}
					else if (!RegisterHotKey(windowHook, id, fsModifier, key))
					{
						if (notify)
							MessageBox.Show($"Failed to hook hotkey to:\n\n" +
								$"WinHook: {windowHook}\n" +
								$"Id: {id}\n" +
								$"FSModifier: {passedFSModifier}\n" +
								$"Key: {passedKey}\n\n" +
								$"Please try again later, or use different inputs.",
								"Hotkey Failed To Hook", MessageBoxButtons.OK, MessageBoxIcon.Error);

						enabled = false;
					}
					else
					{
						MessageBox.Show("No action was invoked and no hook was registered/created.", "Nothing Happened",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else if (enabled)
					MessageBox.Show("Cannot hook the same hotkey to the same window twice.", "Cannot Hook Twice",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		public void Unhook(bool notify = true)
		{
			if (!removed)
			{
				if (enabled)
				{
					if (notify)
						MessageBox.Show($"Successfully unhooked hotkey from:\n\n" +
							$"WinHook: {windowHook}\n" +
							$"Id: {id}\n" +
							$"FSModifier: {passedFSModifier}\n" +
							$"Key: {passedKey}\n\n" +
							$"You can use Hook() and Unhook() to toggle it, and Remove() to remove it.",
							"Hotkey Successfully Unhooked", MessageBoxButtons.OK, MessageBoxIcon.Information);

					UnregisterHotKey(windowHook, id);
					enabled = false;
				}
				else if (!enabled)
					MessageBox.Show("Cannot unhook the same hotkey frome the same window twice.", "Cannot Unhook Twice",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		public void Remove(bool notify = true)
		{
			if (!removed)
			{
				if (notify)
					MessageBox.Show($"Successfully unhooked and deleted hotkey:\n\n" +
						$"WinHook: {windowHook}\n" +
						$"Id: {id}\n" +
						$"FSModifier: {passedFSModifier}\n" +
						$"Key: {passedKey}",
						"Hotkey Successfully Unhooked and Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

				removed = true;
				if (enabled) UnregisterHotKey(WindowHook, id);
			}
			else
			{
				MessageBox.Show("This hotkey was already removed. You cannot remove it again.",
					"Hotkey Already Removed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public static Hotkey Create(IntPtr winHook, int id, FSModifiers fsModifier, Keys key)
		{
			return new Hotkey(winHook, id, fsModifier, key);
		}
	}
}
