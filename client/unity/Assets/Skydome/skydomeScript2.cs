using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class skydomeScript2 : MonoBehaviour {
    
    
    public Light sunLight;
    public Camera cam;
    public Camera skyDomeCamera;
    Sun sunlightScript;
    public bool debug = false;

    public float JULIANDATE = 150;
    public float LONGITUDE = 0.0f;
    public float LATITUDE = 0.0f;
    public float MERIDIAN = 0.0f;
	public float DAY_SPEED = 0.3f;
    public float TIME = 8.0f;
    public float m_fTurbidity = 2.0f;

	public float skyHeight = -6.0f;

    public float cloudSpeed1 = 1.0f;
    public float cloudSpeed2 = 1.5f;
    public float cloudHeight1 = 12.0f;
    public float cloudHeight2 = 13.0f;
    public float cloudTint = 1.0f;

    Vector4 vSunColourIntensity = new Vector4(1f, 1f, 1f, 100);
    Vector4 vBetaRayleigh = new Vector4();
    Vector4 vBetaMie = new Vector4();
    Vector3 m_vBetaRayTheta = new Vector3();
    Vector3 m_vBetaMieTheta = new Vector3();
    
    public float m_fRayFactor = 1000.0f;
	public float m_fMieFactor =  0.7f;
    public float m_fDirectionalityFactor = 0.6f;
    public float m_fSunColorIntensity = 1.0f;
    
	void Start () {
        sunlightScript = sunLight.GetComponent(typeof(Sun)) as Sun;
	}
	
	void OnEnable () {
        sunlightScript = sunLight.GetComponent(typeof(Sun)) as Sun;
	}
	
    // Update is called once per frame
    void Update()
    {
		this.TIME += (float)Time.deltaTime * DAY_SPEED;
		if (this.TIME > 25.0f) {
			this.TIME -= 25.0f;
		}
        calcAtmosphere();
        Vector3 sunLightD = sunLight.transform.TransformDirection(Vector3.forward);
        Vector3 pos = cam.transform.position;
        transform.position = new Vector3(pos.x, this.skyHeight, pos.z);
        renderer.sharedMaterial.SetVector("vBetaRayleigh", vBetaRayleigh);
        renderer.sharedMaterial.SetVector("BetaRayTheta", m_vBetaRayTheta);
        renderer.sharedMaterial.SetVector("vBetaMie", vBetaMie);                     
        renderer.sharedMaterial.SetVector("BetaMieTheta", m_vBetaMieTheta);
        renderer.sharedMaterial.SetVector("g_vEyePt",  pos);
        renderer.sharedMaterial.SetVector("LightDir", sunLightD);
        renderer.sharedMaterial.SetVector("g_vSunColor", sunlightScript.m_vColor);
        renderer.sharedMaterial.SetFloat("DirectionalityFactor", m_fDirectionalityFactor);
        renderer.sharedMaterial.SetFloat("SunColorIntensity", m_fSunColorIntensity);
        renderer.sharedMaterial.SetFloat("tint", cloudTint);
        renderer.sharedMaterial.SetFloat("cloudSpeed1", cloudSpeed1);
        renderer.sharedMaterial.SetFloat("cloudSpeed2", cloudSpeed2);
        renderer.sharedMaterial.SetFloat("plane_height1", cloudHeight1);
        renderer.sharedMaterial.SetFloat("plane_height2", cloudHeight2);
	}
    void calcAtmosphere()
    {
        calcRay();
        CalculateMieCoeff();
    }
    void calcRay()
    {
       
	    const float n  = 1.00029f;		//Refraction index for air
	    const float N  = 2.545e25f;		//Molecules per unit volume
	    const float pn = 0.035f;		//Depolarization factor

        float fRayleighFactor = m_fRayFactor * (Mathf.Pow(Mathf.PI, 2.0f) * Mathf.Pow(n * n - 1.0f, 2.0f) * (6 + 3 * pn)) / ( N * ( 6 - 7 * pn ) );
        
	    m_vBetaRayTheta.x = fRayleighFactor / ( 2.0f * Mathf.Pow( 650.0e-9f, 4.0f ) );
	    m_vBetaRayTheta.y = fRayleighFactor / ( 2.0f * Mathf.Pow( 570.0e-9f, 4.0f ) );
	    m_vBetaRayTheta.z = fRayleighFactor / ( 2.0f * Mathf.Pow( 475.0e-9f, 4.0f ) );

        vBetaRayleigh.x = 8.0f * fRayleighFactor / (3.0f * Mathf.Pow(650.0e-9f, 4.0f));
        vBetaRayleigh.y = 8.0f * fRayleighFactor / (3.0f * Mathf.Pow(570.0e-9f, 4.0f));
        vBetaRayleigh.z = 8.0f * fRayleighFactor / (3.0f * Mathf.Pow(475.0e-9f, 4.0f));
    }
    void CalculateMieCoeff()
    {
        float[] K =new float[3];
        K[0]=0.685f;  
        K[1]=0.682f;
        K[2]=0.670f;

	    float c = ( 0.6544f * m_fTurbidity - 0.6510f ) * 1e-16f;	//Concentration factor

	    float fMieFactor = m_fMieFactor * 0.434f * c * 4.0f * Mathf.PI * Mathf.PI;

	    m_vBetaMieTheta.x = fMieFactor / ( 2.0f * Mathf.Pow( 650e-9f, 2.0f ) );
	    m_vBetaMieTheta.y = fMieFactor / ( 2.0f * Mathf.Pow( 570e-9f, 2.0f ) );
	    m_vBetaMieTheta.z = fMieFactor / ( 2.0f * Mathf.Pow( 475e-9f, 2.0f ) );

        vBetaMie.x = K[0] * fMieFactor / Mathf.Pow(650e-9f, 2.0f);
        vBetaMie.y = K[1] * fMieFactor / Mathf.Pow(570e-9f, 2.0f);
        vBetaMie.z = K[2] * fMieFactor / Mathf.Pow(475e-9f, 2.0f);
    }
 
}
