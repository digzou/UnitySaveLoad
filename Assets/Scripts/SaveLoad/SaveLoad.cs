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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;
using System.Reflection;

public static class SaveLoad
{
	private static readonly string basePath = Application.persistentDataPath + "/";
	private static readonly string GameSettingsFileName = "gamesettingsfile";
	private static readonly string PlayerInfoFileName = "playerinfofile";

	private static PersistentGameSettings gameSettings;
	private static PersistentPlayerInfo playerInfo;

	public static void LoadGame ()
	{
		Load <PersistentGameSettings> ();
		Load <PersistentPlayerInfo> ();
	}

	public static void SaveGame ()
	{
		Save <PersistentGameSettings> ();
		Save <PersistentPlayerInfo> ();
	}

	private static void Load<T> ()
	{
		string filename = getFileName <T> ();

		if (filename != null && File.Exists (basePath + filename)) {
			FileStream file = File.Open (basePath + filename, FileMode.Open);
			try {
				BinaryFormatter bf = new BinaryFormatter ();
				bf.Binder = new VersionDeserializationBinder (); 

				if (typeof(T) == typeof(PersistentGameSettings))
					SaveLoad.gameSettings = bf.Deserialize (file) as PersistentGameSettings;
				else if (typeof(T) == typeof(PersistentPlayerInfo))
					SaveLoad.playerInfo = bf.Deserialize (file) as PersistentPlayerInfo;
				else
					Debug.LogError ("Couldnt load class of type : " + typeof(T));

			} catch (SerializationException e) {
				
				Debug.LogError ("Load Exceptions for " + filename + " msg: " + e.Message);

			} finally {
				if (file != null)
					file.Close ();
			}

		} else {
			if (typeof(T) == typeof(PersistentGameSettings))
				SaveLoad.gameSettings = new PersistentGameSettings ();
			else if (typeof(T) == typeof(PersistentPlayerInfo))
				SaveLoad.playerInfo = new PersistentPlayerInfo ();
			else
				Debug.LogError ("Cannot create new " + typeof(T));
		}
	}

	private static void Save<T> ()
	{
		string filename = getFileName<T> ();

		if (filename == null) {
			Debug.LogError ("Save failed. No file found of type " + typeof(T));
			return;
		}

		if (!string.IsNullOrEmpty (basePath)) {
			Directory.CreateDirectory (basePath);
			FileStream file = File.Create (basePath + filename);
			try {

				BinaryFormatter bf = new BinaryFormatter ();
				bf.Serialize (file, getFile<T> ());
	
			} catch (SerializationException e) {
				
				Debug.LogError ("Save Exception for " + filename + " msg: " + e.Message);

			} finally {

				if (file != null)
					file.Close ();
			}
		}
	}

	private static string getFileName<T> ()
	{
		if (typeof(T) == typeof(PersistentGameSettings))
			return GameSettingsFileName;
		else if (typeof(T) == typeof(PersistentPlayerInfo))
			return PlayerInfoFileName;
		else
			return  null;
	}

	private static T getFile<T> ()
	{
		if (typeof(T) == typeof(PersistentGameSettings))
			return (T)Convert.ChangeType (gameSettings, typeof(PersistentGameSettings));
		else if (typeof(T) == typeof(PersistentPlayerInfo))
			return (T)Convert.ChangeType (playerInfo, typeof(PersistentPlayerInfo));
		else
			return  default(T);
	}

	#region PROPERTIES

	public static PersistentGameSettings GameSettings {
		get { return gameSettings; }
	}

	public static PersistentPlayerInfo PlayerInfo {
		get { return playerInfo; }
	}

	#endregion PROPERTIES
}

public sealed class VersionDeserializationBinder : SerializationBinder
{
	public override Type BindToType (string assemblyName, string typeName)
	{ 
		Type typeToDeserialize = null; 

		if (!string.IsNullOrEmpty (assemblyName) && !string.IsNullOrEmpty (typeName)) { 
			assemblyName = Assembly.GetExecutingAssembly ().FullName; 
			typeToDeserialize = Type.GetType (String.Format ("{0}, {1}", typeName, assemblyName)); 
		} 

		return typeToDeserialize; 
	}
}