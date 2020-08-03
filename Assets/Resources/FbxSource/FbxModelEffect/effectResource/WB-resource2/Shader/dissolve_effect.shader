Shader "Custom/dissolve_effect" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_DissolveColor("Dissolve Color", Color) = (1,1,1,1)
		_Progress("Progress", Range (0, 1)) = 0
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
		_DissolveSrc ("DissolveSrc", 2D) = "white" {}
	}

	SubShader 
	{ 
		Tags { "RenderType"="Opaque" "Queue"="Transparent"}
		Cull Off

		Pass 
		{
			ZWrite On
			ColorMask 0
		}

		CGPROGRAM
		#pragma surface surf NoLight decal:add

		fixed4 _Color;
		fixed4 _DissolveColor;
		fixed _Progress;
		sampler2D _MainTex;
		sampler2D _DissolveSrc;

		half4 LightingNoLight (SurfaceOutput s, half3 lightDir, half atten)
		{
			return 0;
		}

		struct Input 
		{
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			fixed4 ClipTex = tex2D (_DissolveSrc, IN.uv_MainTex);
			fixed ClipAmount = ClipTex.r - _Progress;

			if (ClipAmount < 0)
			{
				clip(-1);
			}
			else
			{
				fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
				if ((ClipAmount > 0.1) || (_Progress == 0))
				{
					o.Emission = (tex.rgb + _Color.rgb) * _Color.a * tex.a;
					o.Alpha = tex.a * _Color.a;
				}
				else
				{
					fixed4 dissolveColor = _DissolveColor * ClipAmount*10 * tex.a;
					o.Emission = dissolveColor.rgb;
					o.Alpha = ClipAmount*10*tex.a;
				}
			}
		}
		ENDCG
	}

	FallBack "Specular"
}
