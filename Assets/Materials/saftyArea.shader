﻿Shader "safty-area"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OuterRadius ("OuterRadius", Float) = 12.0
		_InnerRadius ("InnerRadius", Float) = 10.0
	}
	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" "IgnoreProjector" = "True" }
		
		Cull Off ZWrite Off ZTest Always
		
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR0;
				float2 worldCoord : COLOR1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				float4 worldCoord = mul(unity_ObjectToWorld, v.vertex);
				o.worldCoord = float2(worldCoord.x, worldCoord.y);
				
				o.color = v.color;
				return o;
			}
			
			sampler2D _MainTex;
			float _InnerRadius;
			float _OuterRadius;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 coord = i.worldCoord;
				float dist = sqrt(coord.x * coord.x + coord.y * coord.y);
				float rate = (clamp(dist, _InnerRadius, _OuterRadius) - _InnerRadius) / (_OuterRadius - _InnerRadius);
				float4 color = float4(1.0f, 1.0f, 1.0f, rate) * i.color;
				return color;
			}
			ENDCG
		}
	}
}
