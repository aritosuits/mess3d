
var worldRotateSpeed = 0.2f;
var Player : Transform;

function Update() {
	var rot : Vector3 = Vector3(0,0,0);

	var moved = false;

    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) { rot.x = -1; moved = true; }
    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) { rot.x = 1; moved = true; }
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) { rot.y = -1; moved = true; }
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) { rot.y = 1; moved = true; }

	if (moved) {
		rot = Vector3.Normalize(rot);
		rot *= worldRotateSpeed * Time.deltaTime;
		transform.Rotate(rot);

		rot = Vector3(rot.y, rot.z, -rot.x);
		Player.forward = rot;
	}

//    if (transform.rotation.x > 360) transform.rotation.x -= 360;
//    if (transform.rotation.x < 0) transform.rotation.x += 360;
//    if (transform.rotation.z > 360) transform.rotation.z -= 360;
//    if (transform.rotation.z < 0) transform.rotation.z += 360;
}
