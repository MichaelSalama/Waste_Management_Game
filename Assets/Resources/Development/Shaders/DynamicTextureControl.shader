// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// unlit, vertex color, alpha blended, offset uv's
// cull off

Shader "Custom/DynamicTextureControl"
{
	Properties
	{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_DiscardPlaneBegin("Discard Plane Begin", Vector) = (0, 0, 0, 0)
		_DiscardPlaneMoving("Discard Plane Moving", Vector) = (0, 0, 0, 0)
		_DiscardPlaneEnd("Discard Plane End", Vector) = (0, 0, 0, 0)
		_UseDiscardPlane("Use Discard Plane", Float) = 0
		_DiscardedPixelsColorBegin("Discarded Pixel Color Begin", Color) = (0, 0, 0, 0)
		_DiscardedPixelsColorEnd("Discarded Pixel Color End", Color) = (0, 0, 0, 0)
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off Lighting Off Cull Off Fog{ Mode Off } Blend SrcAlpha OneMinusSrcAlpha
		LOD 110

		Pass
		{
		CGPROGRAM
#pragma vertex VertexMain
#pragma fragment FragmentMain 
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _DiscardPlaneBegin;
			float4 _DiscardPlaneMoving;
			float4 _DiscardPlaneEnd;
			fixed4 _DiscardedPixelsColorBegin;
			fixed4 _DiscardedPixelsColorEnd;
			float _UseDiscardPlane;

			struct VertexShaderInput
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct VertexToFragment
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
				float3 fragPos : TEXCOORD1;
			};

			VertexToFragment VertexMain(VertexShaderInput v)
			{
				VertexToFragment o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.fragPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				return o;
			}
			
			fixed4 FragmentMain(VertexToFragment i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.texcoord) * i.color;
				fixed4 discardedColBegin = _DiscardedPixelsColorBegin;
				fixed4 discardedColEnd = _DiscardedPixelsColorEnd;
				discardedColBegin.a = col.a;
				discardedColEnd.a = col.a;


				if (_UseDiscardPlane > 0) 
				{				
					float distanceBegin = dot(i.fragPos, _DiscardPlaneBegin.xyz) + _DiscardPlaneBegin.w;
					float distanceMoving = dot(i.fragPos, _DiscardPlaneMoving.xyz) + _DiscardPlaneMoving.w;
					float distanceEnd = dot(i.fragPos, _DiscardPlaneEnd.xyz) + _DiscardPlaneEnd.w;

					if (distanceMoving < 0 && distanceBegin > 0)
					{
						return discardedColBegin;
					}
					else if (distanceMoving > 0 && distanceEnd < 0)
					{
						return discardedColEnd;
					}
				}

				return col;
			}

			ENDCG
		}
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Cull Off Fog{ Mode Off }
		LOD 100

		BindChannels
		{
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
			Bind "Color", color
		}

		Pass
		{
			Lighting Off
			SetTexture[_MainTex]{ combine texture * primary }
		}
	}
}