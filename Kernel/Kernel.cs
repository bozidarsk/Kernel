using System;
using System.Runtime;
using System.Runtime.InteropServices;

using Kernel.IO;
using Kernel.Interrupts;

namespace Kernel;

public static class Kernel 
{
	private static void SetupInterrupts() 
	{
		PIC.Remap();

		InterruptDescriptor[] entries = new InterruptDescriptor[256];

		for (int i = 0; i < 256; i++)
			entries[i] = new InterruptDescriptor(
				i,
				InterruptFlags.Interrupt | InterruptFlags.Present,
				(r, e) => DefaultInterruptHandlers.Unused(r, e)
			);

		const int offset = PIC.Offset;
		entries[offset + 0] = new InterruptDescriptor(offset + 0, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 0, r, e));
		entries[offset + 1] = new InterruptDescriptor(offset + 1, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 1, r, e));
		entries[offset + 2] = new InterruptDescriptor(offset + 2, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 2, r, e));
		entries[offset + 3] = new InterruptDescriptor(offset + 3, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 3, r, e));
		entries[offset + 4] = new InterruptDescriptor(offset + 4, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 4, r, e));
		entries[offset + 5] = new InterruptDescriptor(offset + 5, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 5, r, e));
		entries[offset + 6] = new InterruptDescriptor(offset + 6, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 6, r, e));
		entries[offset + 7] = new InterruptDescriptor(offset + 7, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 7, r, e));
		entries[offset + 8] = new InterruptDescriptor(offset + 8, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 8, r, e));
		entries[offset + 9] = new InterruptDescriptor(offset + 9, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 9, r, e));
		entries[offset + 10] = new InterruptDescriptor(offset + 10, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 10, r, e));
		entries[offset + 11] = new InterruptDescriptor(offset + 11, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 11, r, e));
		entries[offset + 12] = new InterruptDescriptor(offset + 12, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 12, r, e));
		entries[offset + 13] = new InterruptDescriptor(offset + 13, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 13, r, e));
		entries[offset + 14] = new InterruptDescriptor(offset + 14, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 14, r, e));
		entries[offset + 15] = new InterruptDescriptor(offset + 15, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.InterruptRequest(offset + 15, r, e));

		entries[0x00] = new InterruptDescriptor(0x00, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x00, r, e));
		entries[0x01] = new InterruptDescriptor(0x01, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x01, r, e));
		entries[0x02] = new InterruptDescriptor(0x02, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x02, r, e));
		entries[0x03] = new InterruptDescriptor(0x03, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x03, r, e));
		entries[0x04] = new InterruptDescriptor(0x04, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x04, r, e));
		entries[0x05] = new InterruptDescriptor(0x05, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x05, r, e));
		entries[0x06] = new InterruptDescriptor(0x06, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x06, r, e));
		entries[0x07] = new InterruptDescriptor(0x07, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x07, r, e));
		entries[0x08] = new InterruptDescriptor(0x08, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x08, r, e));
		entries[0x09] = new InterruptDescriptor(0x09, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Unused(r, e));
		entries[0x0a] = new InterruptDescriptor(0x0a, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x0a, r, e));
		entries[0x0b] = new InterruptDescriptor(0x0b, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x0b, r, e));
		entries[0x0c] = new InterruptDescriptor(0x0c, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x0c, r, e));
		entries[0x0d] = new InterruptDescriptor(0x0d, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x0d, r, e));
		entries[0x0e] = new InterruptDescriptor(0x0e, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x0e, r, e));
		entries[0x0f] = new InterruptDescriptor(0x0f, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Unused(r, e));
		entries[0x10] = new InterruptDescriptor(0x10, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x10, r, e));
		entries[0x11] = new InterruptDescriptor(0x11, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x11, r, e));
		entries[0x12] = new InterruptDescriptor(0x12, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x12, r, e));
		entries[0x13] = new InterruptDescriptor(0x13, InterruptFlags.Interrupt | InterruptFlags.Present, (r, e) => DefaultInterruptHandlers.Exception(0x13, r, e));

		new InterruptDescriptorTable(entries).Load();
	}

	[RuntimeExport("kmain")]
	public static unsafe void Main() 
	{
		Console.ForegroundColor = ConsoleColor.White;

		SetupInterrupts();

		// throw new Exception();
	}
}
