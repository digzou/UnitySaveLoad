/*
MIT License

Copyright (c) [2016] [Digvijay Patel https://github.com/digzou]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using UnityEngine;
using System.Collections;

/// <summary>
/// Sample game setting class which persists players' settings viz. sfx, music and app launch count.
/// </summary>
[System.Serializable]
public class PersistentGameSettings
{
	private bool sfx;
	private bool music;
	private int launchCount;

	public PersistentGameSettings ()
	{
		Debug.Log ("Creating Persistent Game Settings");
		Sfx = true;
		Music = true;
	}

	public bool Sfx {
		get {
			return sfx;
		}
		set {
			sfx = value;
		}
	}

	public bool Music {
		get {
			return music;
		}
		set {
			music = value;
		}
	}

	public int LaunchCount {
		get {
			return launchCount;
		}
		set {
			launchCount = value;
		}
	}
}
