// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/testShader1"
{
	Properties
	{

		_Color ("Color", color) = (1.0, 1.0, 1.0, 1.0)

	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			struct appdata {
				float4 vertex : POSITION;
			};

		// short for vertex to fragment
		struct v2f {
			float4 pos : SV_POSITION;
		};

		v2f vert(appdata v) {
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}

		float4 _Color;

		float4 frag(v2f i) : SV_Target{
			return _Color;
		}


			ENDCG
		}
	}
}
