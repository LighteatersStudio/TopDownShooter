using UnityEngine;
using UnityEngine.Rendering;

namespace RayFire
{
    [System.Serializable]
    public class RFParticleNoise
    {
        public bool                       enabled;
        public ParticleSystemNoiseQuality quality;
        public float                      strengthMin;
        public float                      strengthMax;
        public float                      frequency;
        public float                      scrollSpeed;
        public bool                       damping;
        
        // Constructor
        public RFParticleNoise()
        {
            enabled     = false;
            strengthMin = 0.3f;
            strengthMax = 0.6f;
            frequency   = 0.3f;
            scrollSpeed = 0.7f;
            damping     = true;
            quality     = ParticleSystemNoiseQuality.High;
        }
        
        // Copy from
        public void CopyFrom (RFParticleNoise source)
        {
            enabled     = source.enabled;
            strengthMin = source.strengthMin;
            strengthMax = source.strengthMax;
            frequency   = source.frequency;
            scrollSpeed = source.scrollSpeed;
            damping     = source.damping;
            quality     = source.quality;
        }
    }

    [System.Serializable]
    public class RFParticleRendering
    {
        public bool                       castShadows;
        public bool                       receiveShadows;
        public MotionVectorGenerationMode motionVectors;
        public LightProbeUsage            lightProbes;
        public bool                       l;
        public int                        layer;
        public bool                       t;
        public string                     tag; // = "Untagged";
        
        // Constructor
        public RFParticleRendering()
        {
             castShadows    = true;
             receiveShadows = true;
             motionVectors  = MotionVectorGenerationMode.Object;
             lightProbes    = LightProbeUsage.Off;
        }
        
        // Copy from
        public void CopyFrom (RFParticleRendering source)
        {
            castShadows    = source.castShadows;
            receiveShadows = source.receiveShadows;
            motionVectors  = source.motionVectors;
            lightProbes    = source.lightProbes;
            l              = source.l;
            layer          = source.layer;
            t              = source.t;
            tag            = source.tag;
        }
    }
    
    [System.Serializable]
    public class RFParticleDynamicDebris
    {
        public float speedMin;
        public float speedMax;
        public float velocityMin;
        public float velocityMax;
        public float gravityMin;
        public float gravityMax;
        public float rotationSpeed;
        
        // Constructor
        public RFParticleDynamicDebris()
        {
            speedMin      = 1f;
            speedMax      = 4f;
            velocityMin   = 0.5f;
            velocityMax   = 1.5f;
            rotationSpeed = 0.5f;
            gravityMin    = 0.8f;
            gravityMax    = 1.1f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleDynamicDebris source)
        {
            speedMin      = source.speedMin;
            speedMax      = source.speedMax;
            velocityMin   = source.velocityMin;
            velocityMax   = source.velocityMax;
            rotationSpeed = source.rotationSpeed;
            gravityMin    = source.gravityMin;
            gravityMax    = source.gravityMax;
        }
    }
    
    [System.Serializable]
    public class RFParticleDynamicDust
    {
        public float speedMin;
        public float speedMax;
        public float rotation;
        public float gravityMin;
        public float gravityMax;

        // Constructor
        public RFParticleDynamicDust()
        {
            speedMin   = 0.5f;
            speedMax   = 1f;
            rotation   = 0.5f;
            gravityMin = 0.01f;
            gravityMax = 0.6f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleDynamicDust source)
        {
            speedMin   = source.speedMin;
            speedMax   = source.speedMax;
            rotation   = source.rotation;
            gravityMin = source.gravityMin;
            gravityMax = source.gravityMax;
        }
    }
    
    [System.Serializable]
    public class RFParticleEmission
    {
        public RFParticles.BurstType burstType;
        public int                   burstAmount;
        public float                 distanceRate;
        public float                 duration;
        public float                 lifeMin;
        public float                 lifeMax;
        public float                 sizeMin;
        public float                 sizeMax;
        
        // Constructor
        public RFParticleEmission()
        {
            burstType    = RFParticles.BurstType.PerOneUnitSize;
            burstAmount  = 20;
            duration     = 4;
            distanceRate = 1f;
            lifeMin      = 2f;
            lifeMax      = 13f;
            sizeMin      = 0.5f;
            sizeMax      = 2.5f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleEmission source)
        {
            burstType    = source.burstType;
            burstAmount  = source.burstAmount;
            distanceRate = source.distanceRate;
            lifeMin      = source.lifeMin;
            lifeMax      = source.lifeMax;
            sizeMin      = source.sizeMin;
            sizeMax      = source.sizeMax;
        }
    }
    
    [System.Serializable]
    public class RFParticleLimitations
    {
        public int   minParticles;
        public int   maxParticles;
        public bool  visible;
        public int   percentage;
        public float sizeThreshold;

        // Constructor
        public RFParticleLimitations()
        {
            minParticles  = 3;
            maxParticles  = 20;
            visible       = false;
            percentage    = 50;
            sizeThreshold = 0.5f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleLimitations source)
        {
            minParticles  = source.minParticles;
            maxParticles  = source.maxParticles;
            visible       = source.visible;
            percentage    = source.percentage;
            sizeThreshold = source.sizeThreshold;
        }
    }

    [System.Serializable]
    public class RFParticleCollisionDebris
    {
        // death on collision
        // dynamic collision
        
        public enum RFParticleCollisionMatType
        {
            ByPhysicalMaterial = 0,
            ByProperties       = 1
        }
        
        public LayerMask                      collidesWith;
        public ParticleSystemCollisionQuality quality;
        public float                          radiusScale;
        public RFParticleCollisionMatType     dampenType;
        public float                          dampenMin;
        public float                          dampenMax;
        public RFParticleCollisionMatType     bounceType;
        public float                          bounceMin;
        public float                          bounceMax;
        
        [HideInInspector] public bool propertiesSet = false;
        
        // Constructor
        public RFParticleCollisionDebris()
        {
            collidesWith = -1; // -1 Everything, 0 Nothing
            quality      = ParticleSystemCollisionQuality.High;
            radiusScale  = 0.1f;
            dampenType   = RFParticleCollisionMatType.ByPhysicalMaterial;
            dampenMin    = 0.1f;
            dampenMax    = 0.4f;
            bounceType   = RFParticleCollisionMatType.ByPhysicalMaterial;
            bounceMin    = 0.2f;
            bounceMax    = 0.4f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleCollisionDebris source)
        {
            collidesWith  = source.collidesWith;
            quality       = source.quality;
            radiusScale   = source.radiusScale;
            dampenType    = source.dampenType;
            dampenMin     = source.dampenMin;
            dampenMax     = source.dampenMax;
            bounceType    = source.bounceType;
            bounceMin     = source.bounceMin;
            bounceMax     = source.bounceMax;
            propertiesSet = source.propertiesSet;
        }
        
        // Set material properties
        public void SetMaterialProps (RayfireDebris debris)
        {
            // Properties was set. Exclude this step
            if (propertiesSet == true)
                return;
            
            // Do this method once
            propertiesSet = true;
            
            // No need to set collision properties. NO collision expected
            if (debris.collision.collidesWith == 0) // Nothing)
                return;

            // own props should be used
            if (dampenType == RFParticleCollisionMatType.ByProperties && bounceType == RFParticleCollisionMatType.ByProperties)
                return;
            
            // Set collider to take properties
            Collider collider;
            if (debris.rigid == null || debris.rigid.physics.meshCollider == null)
                collider = debris.GetComponent<Collider>();
            else
                collider = debris.rigid.physics.meshCollider;

            // No collider
            if (collider == null)
                return;

            // No collider material
            if (collider.sharedMaterial == null)
                return;
            
            // Set dampen
            if (dampenType == RFParticleCollisionMatType.ByPhysicalMaterial)
            {
                dampenMin = collider.sharedMaterial.dynamicFriction;
                dampenMax = dampenMin * 0.05f + dampenMin;
            }
            
            // Set bounce
            if (bounceType == RFParticleCollisionMatType.ByPhysicalMaterial)
            {
                bounceMin = collider.sharedMaterial.bounciness;
                bounceMax = bounceMin * 0.05f + bounceMin;
            }
        }
    }
    
    [System.Serializable]
    public class RFParticleCollisionDust
    {
        public LayerMask                      collidesWith;
        public ParticleSystemCollisionQuality quality;
        public float                          radiusScale;

        // Constructor
        public RFParticleCollisionDust()
        {
            collidesWith = -1;
            quality      = ParticleSystemCollisionQuality.High;
            radiusScale  = 1f;
        }
        
        // Copy from
        public void CopyFrom (RFParticleCollisionDust source)
        {
            collidesWith = source.collidesWith;
            quality      = source.quality;
            radiusScale  = source.radiusScale;
        }
    }
}


