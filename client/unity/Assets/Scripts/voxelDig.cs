using UnityEngine;
using System.Collections;

public class voxelDig : MonoBehaviour {

    bool dig = false;
    bool place = false;
    bool skipframe = false;

    voxelPlanet vp;

    // Use this for initialization
    void Start () {
        dig = false;
        place = false;
    }
    
    // Update is called once per frame
    void Update () {
        if (!skipframe) {
            vp = voxelPlanet.instance;
            if (Input.GetMouseButton(0)) dig = true;
            if (Input.GetMouseButton(1)) place = true;
            if (dig) {
                skipframe = true;
                int[] coords = findVoxelCoords();
                //print("trying (" + coords[0] + ", " + coords[1] + ", " + coords[2] + ")");
                vp.dig(coords[0], coords[1], coords[2], 0);
                dig = false;
            } else if (place) {
                skipframe = true;
                int[] coords = findVoxelCoords();
                //print("trying (" + coords[0] + ", " + coords[1] + ", " + coords[2] + ")");
                vp.dig(coords[0], coords[1], coords[2], 1);
                place = false;
            }
        }
        skipframe = false;
    }

    int[] findVoxelCoords() {
        return new int[3] {
            (int)(transform.position.x / vp.WORLD_SCALE),
            (int)((15 + transform.position.y) / vp.WORLD_SCALE),
            (int)(transform.position.z / vp.WORLD_SCALE)
        };
    }

}
