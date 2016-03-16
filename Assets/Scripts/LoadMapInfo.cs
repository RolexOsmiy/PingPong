using UnityEngine;
using System.Collections;
using System.IO;

public class LoadMapInfo : MonoBehaviour {

	public GameObject [] cost_mission = new GameObject[5];
	public GUIText[] medal_mission = new GUIText[5];
	public GameObject[] line = new GameObject[5];

	private string MapInfoDataPath; //Полный путь до файла, будет создаваться в функциях
	private string MapInfoDataName = "map_info.ply"; //Название файла с нашими данными
	private int inc_start_anyway = 1;

	public void Edit_Missions_Screen (int cost, int number){
		
	}

	public void WriteData() {
		MapInfoDataPath = Application.dataPath + '/' + MapInfoDataName; //Полный путь к файлу = положение нашей игры + название файла (файл будет сохраняться в папку _Data)
		if(File.Exists(MapInfoDataPath) == false){
		StreamWriter map_infWriter = new StreamWriter(MapInfoDataPath); //Реализует запись в строку и, если нет нашего файла, создаёт его по указанному пути

		map_infWriter.WriteLine(1);

		map_infWriter.Flush(); //Очищает буфер
		map_infWriter.Close(); //Закрывает объект StreamWriter
		}
	}

	public void ReadData_INFMAP() {
		MapInfoDataPath = Application.dataPath +'/'+ MapInfoDataName; //Восстанавливаем полный путь к файлу
		StreamReader map_infReader = new StreamReader(MapInfoDataPath); //Реализует чтение строки
	
		var inc_start_anyway = map_infReader.ReadLine();

		if (inc_start_anyway == null) {
			WriteData ();
		}

		var info_for_player_cost_1 = map_infReader.ReadLine();//инфа о lvl 1
		var info_for_player_cost_2 = map_infReader.ReadLine();//инфа о lvl 2
		var info_for_player_cost_3 = map_infReader.ReadLine();//инфа о lvl 3
		var info_for_player_cost_4 = map_infReader.ReadLine();//инфа о lvl 4
		var info_for_player_cost_5 = map_infReader.ReadLine();//инфа о lvl 5

		//Debug.Log (line1);
		//Debug.Log (line2);

		//map_infWriter.Flush();
		//map_infWriter.Close(); //Закрываем объект OpenText
	}

	void Start () {
		WriteData ();
		ReadData_INFMAP ();
	}

}
