using System;
using flyingRats.RunSetup;

namespace flyingRats
{
#if WINDOWS || XBOX
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main(string[] args)
		{
			RunParameters rp = null;
			using (FrmSetup frm = new FrmSetup())
			{
				rp = frm.Execute();
			}
			if (rp != null)
			{
				using (ScaringDefense game = new ScaringDefense(rp))
				{
					game.Run();
				}
			}
		}
	}
#endif
}

