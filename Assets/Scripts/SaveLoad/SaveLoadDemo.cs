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

public class SaveLoadDemo : MonoBehaviour
{
	private void Awake ()
	{
		SaveLoad.LoadGame ();
	}

	private void Start ()
	{
		//Fetch loaded values.
		Debug.Log ("Player Name : " + SaveLoad.PlayerInfo.Name);
		Debug.Log ("Player Highscore : " + SaveLoad.PlayerInfo.Highscore);
		Debug.Log ("Player Level : " + SaveLoad.PlayerInfo.Level);
		Debug.Log ("Is SFX On : " + SaveLoad.GameSettings.Sfx);
		Debug.Log ("Is Music On : " + SaveLoad.GameSettings.Music);

		//Set new values to be saved.
		SaveLoad.PlayerInfo.Name = "Eric Clapton";
		SaveLoad.PlayerInfo.Highscore = 1000;
		SaveLoad.PlayerInfo.Level = 5;
		SaveLoad.GameSettings.Sfx = false;
		SaveLoad.GameSettings.Music = false;
		SaveLoad.GameSettings.LaunchCount++;

		//Values will persist even after new session launched.
	}

	private void OnApplicationQuit ()
	{
		SaveLoad.SaveGame ();
	}
}
