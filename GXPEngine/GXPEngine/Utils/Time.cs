using System.Diagnostics;
using System;

namespace GXPEngine
{
	/// <summary>
	/// Contains various time related functions.
	/// </summary>
	public class Time
	{
		private static int previousTime;

		static Time() {
		}
		
		/// <summary>
		/// Returns the current system time in milliseconds
		/// </summary>
		public static int now {
			get { return System.Environment.TickCount; }
		}

		/// <summary>
		/// Returns this time in milliseconds since the program started		
		/// </summary>
		/// <value>
		/// The time.
		/// </value>
		public static int time {
			get { return (int)(OpenGL.GL.glfwGetTime()*1000); }
        }
        public static float timeS
		{
			get { return (float)OpenGL.GL.glfwGetTime(); }
		}

        private static int previousFrameTime;
		private static float previousFrameTimeS;
        /// <summary>
        /// Returns the time in milliseconds that has passed since the previous frame
        /// </summary>
        /// <value>
        /// The delta time.
        /// </value>
        public static int deltaTime {
			get { 
				return previousFrameTime; 
			}
		}
		public static float deltaTimeS
		{
			get { return previousFrameTimeS; }
		}

		internal static void newFrame() {
			previousFrameTime = time - previousTime;
			previousTime = time;
			previousFrameTimeS = previousFrameTime*.001f;
		}
	}
}

