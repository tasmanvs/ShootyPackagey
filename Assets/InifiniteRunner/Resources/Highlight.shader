Shader "Custom/Highlight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [Toggle] _ShouldHighlight ("Should Highlight", float) = 0
        _Color("Color", Color) = (1, 1, 1, 1)
        _OutlineColor("Outline Color", Color) = (1, 0, 0, 1)
        _OutlineWidth("Outline Width", Range(0, 100)) = 0.5
     }

    SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
            "RenderType" = "Opaque"
		}

		Pass
		{
			Name "OUTLINEPASS"

			ZWrite off

			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
			};

			float4 _OutlineColor;
			float _OutlineWidth;

			v2f vert(appdata IN)
			{
				v2f o;
				// float camDist = distance(UnityObjectToWorldDir(IN.vertex), _WorldSpaceCameraPos);
				IN.vertex.xyz += (IN.normal) * _OutlineWidth;
				o.pos = UnityObjectToClipPos(IN.vertex);
				return o;
			}

			float4 frag(v2f IN) : SV_TARGET
			{
				return _OutlineColor;
			}

			ENDCG
		}

    //      CGPROGRAM
    //     // Physically based Standard lighting model, and enable shadows on all light types
    //     #pragma surface surf Standard fullforwardshadows

    //     // Use shader model 3.0 target, to get nicer looking lighting
    //     #pragma target 3.0

    //     sampler2D _MainTex;

    //     struct Input
    //     {
    //         float2 uv_MainTex;
    //     };

    //     half _Glossiness;
    //     half _Metallic;
    //     fixed4 _Color;

    //     // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
    //     // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
    //     // #pragma instancing_options assumeuniformscaling
    //     UNITY_INSTANCING_BUFFER_START(Props)
    //         // put more per-instance properties here
    //     UNITY_INSTANCING_BUFFER_END(Props)

    //     void surf (Input IN, inout SurfaceOutputStandard o)
    //     {
    //         // Albedo comes from a texture tinted by color
    //         fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
    //         o.Albedo = c.rgb;
    //         // Metallic and smoothness come from slider variables
    //         o.Metallic = _Metallic;
    //         o.Smoothness = _Glossiness;
    //         o.Alpha = c.a;
    //     }
    //     ENDCG
    // }
    // FallBack "Diffuse"
    }
}
