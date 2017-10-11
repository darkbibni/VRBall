Shader "Unlit/NewShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Alpha ("Alpha",Range(0,1)) = 1
		_Color("Color",Color) = (1,1,1,1)
		_Extrude("Extrude",Range(0,5))=1
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			//Cull Off
			//ZTest Always
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha


			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 normals : NORMAL;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 colors : COLOR;

			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			float _Extrude;
			float4 _Color;
			float _Alpha;
			
			v2f vert (appdata v)
			{
				v2f o;
				v.vertex += v.normals * _Extrude;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{

				fixed4 col = tex2D(_MainTex, i.uv);
				col.a = lerp(1, _Alpha, col.rgb);
			
				
				//col.rgb = lerp(col.rgb, _Color,i.uv.y );

				//col.rgb *= _Color;
				//col.rgb = lerp(col.rgb, _Color,saturate( sin(_Time.y)));
				//col.a = saturate(2 * col.a);
				//Lerp(a, b, t);
				//clip(col.r - col.g);

				// apply fog


				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
