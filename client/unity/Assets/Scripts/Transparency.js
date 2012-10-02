#pragma strict

var ALPHA = 0.25;

function Start () {
	this.gameObject.renderer.material.color.a = this.ALPHA;
}
