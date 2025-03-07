Shader "Amazing Assets/Curved World/Tutorial/Unlit Shader"
{
    Properties
    {
        //Paste Curved World material property here////////////////////////////////////
        [CurvedWorldBendSettings] _CurvedWorldBendSettings("2|1|1", Vector) = (0, 0, 0, 0)

        [CurvedWorldBendSettings] _CurvedWorldBendSettings("2|2|1", Vector) = (0, 0, 0, 0)

        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass  
        {
            CGPROGRAM 
            #pragma vertex vert
            #pragma fragment frag 
            #pragma multi_compile_fog
  
            #include "UnityCG.cginc"     
 

			//Paste Curved World definitions and keywords here/////////////////////////
            #define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Z_POSITIVE
            #define CURVEDWORLD_BEND_ID_1
            #pragma shader_feature_local CURVEDWORLD_DISABLED_ON
            #pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
            #include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"

            #define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Z_POSITIVE
            #define CURVEDWORLD_BEND_ID_2
            #pragma shader_feature_local CURVEDWORLD_DISABLED_ON
            #pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
            #include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"

			sampler2D _MainTex;
            float4 _MainTex_ST;

            struct v2f 
            {
				float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                
            };

            v2f vert (appdata_full v)
            {
                v2f o;


				//Paste Curved World vertex transformation here////////////////////////
                #if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
                #ifdef CURVEDWORLD_NORMAL_TRANSFORMATION_ON
                CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v.vertex, v.normal, v.tangent)
                #else
                CURVEDWORLD_TRANSFORM_VERTEX(v.vertex)
                #endif
                #endif

                #if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
                   #ifdef CURVEDWORLD_NORMAL_TRANSFORMATION_ON
                      CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v.vertex, v.normal, v.tangent)
                   #else
                      CURVEDWORLD_TRANSFORM_VERTEX(v.vertex)
                   #endif
                #endif

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
				
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);


                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
