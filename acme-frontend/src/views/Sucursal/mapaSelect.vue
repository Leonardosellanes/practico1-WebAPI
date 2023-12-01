<template>
    
    <a-card v-if="error"> No hay sucursales disponibles </a-card>
    
    <div class="my-card" ref="map"></div>

</template>
  
<script>
import EmpresasController from '../../services/EmpresasController';

    export default{
        data(){
            return{
                map: null,
                geocoder: null,
                address: "",
                center: { lat: -34.901113, lng: -56.164531 },
                loading : true,
                markers : [],
                idEmpresa: 0,
                sucursales: [],         
                error: false,
                selectedSucursal: null        
            }
        },
        mounted(){
            this.idEmpresa = sessionStorage.getItem('empresaId');
            this.buscarEmpresa();
        },
        methods: {
            selectSucursal(id, nombre, tiempoEntrega){
                this.selectedSucursal = {
                    id: id,
                    nombre: nombre,
                    tiempoEntrega: tiempoEntrega
                }
                this.$emit('seleccionar-sucursal', this.selectedSucursal);
            },
            
            buscarEmpresa() {
                this.loading = true;
                EmpresasController.getById(this.idEmpresa).then((response) => {
                    const empresa = response.data;
                    console.log(empresa);
                    this.sucursales = empresa.sucursales;
                    console.log(this.sucursales);

                    if(this.sucursales.length == 0){
                        this.error = true;
                    }else{
                        const ubi = this.sucursales[0].ubicacion;
                        const inicioLatitud = ubi.indexOf("lat: ") + 5;
                        const finLatitud = ubi.indexOf(", lng:");
                        const latitud = parseFloat(ubi.substring(inicioLatitud, finLatitud));

                        const inicioLongitud = ubi.indexOf("lng: ") + 5;
                        const longitud = parseFloat(ubi.substring(inicioLongitud));

                        this.center = { lat: latitud, lng: longitud };
                    

                        this.map = new window.google.maps.Map(this.$refs.map, {
                            center: this.center,
                            zoom: 10,
                        });

                        this.sucursales.forEach((sucursal) => {
                            const inicioLatitud = sucursal.ubicacion.indexOf("lat: ") + 5;
                            const finLatitud = sucursal.ubicacion.indexOf(", lng:");
                            const latitud = parseFloat(sucursal.ubicacion.substring(inicioLatitud, finLatitud));

                            const inicioLongitud = sucursal.ubicacion.indexOf("lng: ") + 5;
                            const longitud = parseFloat(sucursal.ubicacion.substring(inicioLongitud));

                            const ubi = { lat: latitud, lng: longitud };
                            console.log('ubi: ' + ubi);
                            const marker = new window.google.maps.Marker({
                                position: ubi,
                                map: this.map,
                                title: sucursal.nombre,
                            });

                            

                            marker.addListener('click', () => {
                                this.selectSucursal(sucursal.id, sucursal.nombre, sucursal.tiempoEntrega);
                            });

                            this.markers.push(marker);
                        });
                        this.loading = false;
                    }

                })
                .catch ((error) => {
                console.error('Error al obtener empresa:', error);
                });
            },
        },

    }
</script>

<style scoped>
    .my-card {
        border-radius: 24px !important;
        height: 600px;
    }

</style>