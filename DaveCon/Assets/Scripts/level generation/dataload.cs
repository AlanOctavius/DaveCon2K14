using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public class dataload : MonoBehaviour {
	public int number_of_rooms;
	public int number_of_layers;
	public int number_of_room_templates;

	// Use this for initialization
	void Start () {
		int xstart = 0;
		int ystart = 0;
		for (int nn = 0; nn<number_of_layers; nn++) {
						for (int n = 0; n<number_of_rooms; n++) {
								roombuild ((xstart + n * 10), ystart + ((nn - 1) * 8)); //builds a single 8x10 room in the level starting at 0,0
						}
				}
	}
	
	// Update is called once per frame
	void Update () {
		int xstart = 0;
		int ystart = 0;
		if (Input.anyKeyDown == true) {
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Block");
			foreach (GameObject target in gameObjects) {
				GameObject.Destroy(target);
			}
			for (int nn = 0; nn<number_of_layers;nn++){
				for (int n = 0; n<number_of_rooms; n++) {
						roombuild ((xstart+n*10), ystart + ((nn-1)*8)); //builds a single 8x10 room in the level starting at 0,0
				}
			}
		}
	
	}





	new void roombuild(int xstart, int ystart){
		
		
				string[] loadedmapdata = dataloader (); //loads the room data required for the room
				string[] mapvalues = loadedmapdata [0].Split (','); //splits up the room data strings 
		
				//Debug.Log (mapvalues [1]); //debug test
		
				Dictionary<int, string[]> mapdata = new Dictionary<int, string[]> (); //creates a dictionary with an entry for each line of the room string assigned to a int
		
				for (int n = 0; n < Convert.ToInt32(mapvalues[1]); n++) { //this is the loop that actually creates said dictionary
						string[] dictionaryData = loadedmapdata [n + 1].Split (',');
						mapdata.Add (n, dictionaryData);
				}
		
				//Debug.Log (mapvalues [1]); //debug test
		placeroomblocks (mapdata,mapvalues,xstart,ystart); //places the blocks for the room in the map

		}

	new string[] dataloader(){
		int randomroom = UnityEngine.Random.Range (1, number_of_room_templates+1); //THIS NEESD TO BE CHANGED IF FILES ARE ADDED!!!!!!!
		TextAsset rawdata = (TextAsset)Resources.Load ("Level Generation/room" + randomroom, typeof(TextAsset));
		string[] mapdata = rawdata.text.Split ('\n');
		return mapdata;
	}

	new void placeroomblocks(Dictionary<int, string[]> thedata, string[] roomsizedata, int xstart, int ystart){

		for (int n = 0; n < Convert.ToInt32 (roomsizedata[1]); n++) { //for loop that builds the block objects
			
			//Debug.Log (mapdata[0]);
			string[] mapplot = thedata [n]; //horizontal segment of room data from the dictionary mapdata is asigned to a string
			for (int nn = 0; nn < Convert.ToInt32 (roomsizedata[0]); nn++) {
				int ylocation = Convert.ToInt32 (roomsizedata[0]) - n; //(NEEDED TO MIRROR ALONG THE Y axis, otherwise it builds it wrong)
				if (mapplot [nn] == "01") { //IF there is a "1" in the data string
					Instantiate (Resources.Load ("Level Generation/Prefabs/Block"), new Vector3 (nn+xstart, ylocation+ystart, 0), Quaternion.identity); //Create a block at the set position
				}

				else if (mapplot[nn] == "SA"){
					Debug.Log ("Loading special area");
					loadspecialarea(nn, ylocation,xstart,ystart);
				}
			}
		} //end of object building for loop
	}

	new void loadspecialarea(int xlocation, int ylocation, int xstart, int ystart){

/*		if (ylocation == 0) {
						TextAsset rawspecial = (TextAsset)Resources.Load ("Level Generation/special areas/g1", typeof(TextAsset)); //load a GROUND special area
				} else {
						TextAsset rawspecial = (TextAsset)Resources.Load ("Level Generation/special areas/g1", typeof(TextAsset));
				}*/
		int randomarea = UnityEngine.Random.Range (1, 3);
		//Debug.Log (randomarea);                                                   //THIS NEESD TO BE CHANGED IF FILES ARE ADDED!!!!!!!
		//int filenumber = DirCount ();
		TextAsset rawspecial = (TextAsset)Resources.Load ("Level Generation/special areas/ground/g" + randomarea, typeof(TextAsset));
		string[] splitdata = rawspecial.text.Split ('\n'); //split up the area data
		Dictionary<int,string[]> areadata = new Dictionary<int,string[]> (); //make a blank dictionary

		for (int n = 0; n < 3; n++) { //populates the dictionary with data
			string[] dictionaryData = splitdata[n].Split (',');
			areadata.Add (n, dictionaryData);
		}
		string[] testy = areadata [2];
		//Debug.Log (testy);

		buildspecialarea (areadata, xlocation, ylocation,xstart,ystart);
		//return void;
	}

	new void buildspecialarea(Dictionary<int,string[]> thedata, int xloc, int yloc,int xstart, int ystart){

		for (int nn = yloc; nn > yloc - 3; nn-- ){
			string[] rowdata = thedata[yloc-nn];
			for (int n = xloc; n < xloc + 5; n++){
				if (rowdata [n-xloc] == "01") { //IF there is a "1" in the data string
					Instantiate (Resources.Load ("Level Generation/Prefabs/Block"), new Vector3 (n+xstart, nn+ystart, 0), Quaternion.identity); //Create a block at the set position
				}

			}
		}
		//return void;
	}
/*	public static long DirCount(DirectoryInfo d)
	{
		long i = 0;
		// Add file sizes.
		FileInfo[] fis = d.GetFiles();
		foreach (FileInfo fi in fis)
		{
			i++;
		}
		
		return i;
	}
*/	
}
