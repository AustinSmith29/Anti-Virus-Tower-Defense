using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour {

    public List<GameObject> towerPrefabs;  // The available towers to be placed.
    public int selectedTower = 0;
    public Button basicTowerButton;
    public Button powerTowerButton;
    public Button scatterTowerButton;
    // Use this for initialization
    void Start () {
        Button btn_basic = basicTowerButton.GetComponent<Button>();
        btn_basic.onClick.AddListener(selectBasic);

        Button btn_power = powerTowerButton.GetComponent<Button>();
        btn_power.onClick.AddListener(selectPower);

        Button btn_scatter = scatterTowerButton.GetComponent<Button>();
        btn_scatter.onClick.AddListener(selectScatter);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            int layerMask = 1 << 8; // Mask the 8th layer (Tiles Layer) so we don't have hits on tower colliders.
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 12, layerMask);
            if (hit)
            { 
                if (hit.collider.gameObject.tag != "Tower" && hit.collider.gameObject.GetComponent<TileScript>().canPlaceTower
                    && GameState.currency >= towerPrefabs[selectedTower].GetComponent<BasicTower>().cost)
                {
                    Vector3 tilePosition = new Vector3(hit.collider.transform.position.x + .32f, hit.collider.transform.position.y -.32f, 0);
                    hit.collider.gameObject.GetComponent<TileScript>().canPlaceTower = false;
                    placeTower(towerPrefabs[selectedTower], tilePosition);
                    GameState.currency -= towerPrefabs[selectedTower].GetComponent<BasicTower>().cost;
                }
                else
                {
                    print("Tower already at that location");
                    print(hit.collider.name);
                }
            }
        }
	}

    private void placeTower(Object towerPrefab, Vector3 position)
    {
        var tower = PrefabUtility.InstantiatePrefab(towerPrefab) as GameObject;
        tower.GetComponent<BasicTower>().Init(position);
        tower.layer = 1;
    }

    void selectBasic()
    {
        selectedTower = 0;
    }

    void selectPower()
    {
        selectedTower = 1;
    }

    void selectScatter()
    {
        selectedTower = 2;
    }
}
