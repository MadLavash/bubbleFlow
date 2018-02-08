// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.


Shader "Rodikov/emissionShader" 
{
	Properties 
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1)
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#include "UnityCG.cginc"

		#pragma surface surf Standard fullforwardshadows vertex:vert
		#pragma target 3.0

		fixed4 _Color;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float highlight;
		};
	
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input,o);

			float3 worldPos = UnityObjectToClipPos(v.vertex).xyz;
			//o.highlight = max(saturate(-worldPos.y),  abs(worldPos.x));
			o.highlight = saturate(-worldPos.y);	
		}

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) 
		{
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			o.Albedo = c.rgb;
			o.Emission.rgb = IN.highlight;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
