﻿using UnityEngine;
using System.Collections;

public class MenuAnim2 : MonoBehaviour {


	void Update()
	{

		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y + Time.deltaTime * 0.3f, this.gameObject.transform.position.z);

		if (this.gameObject.transform.position.y > 13.82f) {
			this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, -10.88f, this.gameObject.transform.position.z);
		}
	}
}
