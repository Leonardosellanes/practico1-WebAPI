<template>
    <a-row justify="space-between" style="width: 97%">
        <a-card style="width: 65%">
            <div v-if="!moduloPago">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Productos" />
                
            <a-list>
                <a-list-item v-for="item in productos">
                    <a-row justify="space-between" style="width: 100%">
                        <a-col :span="12">
                            <a-space direction="horizontal" style="width: 100%; align-items: flex-start;">
                                <img alt="example" style="object-fit: cover; height: 100px; margin-right: 15px;"
                                    :src="'data:image/png;base64,' + item.pOs.base64" />
                                <a-space direction="vertical">
                                    <h2> {{ item.pOs.titulo}} </h2>
                                    {{ item.pOs.descripcion }}
                                </a-space>
                            </a-space>
                        </a-col>
                        <a-col :span="4">
                            <a-space direction="horizontal" style="width: 100%; margin-top: 70px; align-items: center;">                  
                            Cantidad:          

                            <a-input-number 
                                style="text-align: center; width: 50px;"
                                id="inputNumber" 
                                v-model:value="item.cantidad" 
                                :min="1" 
                                :max="10" 
                                :disabled="!enableCant" 
                            />

                            <EditOutlined 
                                v-if="!enableCant"
                                :style="{ fontSize: '24px', color: 'gray' }" 
                                @click="enableCant = true"
                            />

                            <CheckOutlined 
                                v-if="enableCant"
                                :style="{ fontSize: '24px', color: 'gray' }" 
                                @click="guardarCant(item)"
                            />

                            </a-space>

                        </a-col>
                        <a-col :span="4">
                            <a-space direction="vertical" style="width: 100%;">
                                <a-space direction="vertical" style="width: 100%; margin-bottom: 20px; align-items: end;">
                                    <CloseOutlined 
                                        :style="{ fontSize: '24px', color: 'gray' }" 
                                        @click="quitarProducto(item.id)"
                                    />
                                </a-space>  
                                <a-space direction="vertical" style="width: 100%; margin-top: 20px; align-items: end;">
                                    
                                    <h3>$ {{ item.pOs.precio * item.cantidad }} </h3> 
                                </a-space>
                            </a-space>
                        </a-col>
                    </a-row>
                    
                    
                    
                </a-list-item>
            </a-list>
            </div>
            <PagosView v-if="moduloPago" :dataCarrito="dataCarrito"></PagosView>
        </a-card>
        <a-card style="width: 32%">
            <a-page-header style="border: 1px solid rgb(235, 237, 240)" title="Resumen de compra" />

            <a-list>
                <a-list-item>
                    <a-row class="m-4" justify="space-between" style="width: 90%">
                        <h4>Productos</h4> 
                        <p >
                            $ {{ totalProductos }}
                        </p> 
                    </a-row>
                </a-list-item>
                <a-list-item>
                    <a-row class="m-4" justify="space-between" style="width: 90%">
                        <h4>Envío</h4> 
                        <p >
                            $ {{ costoEnvio }}
                        </p> 
                    </a-row>
                </a-list-item>

                <a-list-item>
                    <a-row class="m-6" justify="end" style="width: 90%">
                        <h4 class="mx-6">TOTAL:</h4> 
                        <h3>
                            $ {{ totalProductos + costoEnvio }}
                        </h3> 
                    </a-row>
                </a-list-item>
            </a-list>

            <a-card class="mt-4" @click="showModalEntrega = true">
                <a-row justify="space-between" style="width: 100%">
                    <h4>Método de entrega</h4> 
                    <a-space direction="vertical" v-if="dataCarrito.direccionDeEnvio != '' && selectEntrega == 'Domicilio'">
                        <p> Envío a Domicilio </p> 
                        <p>{{ dataCarrito.direccionDeEnvio }}</p>
                    </a-space>

                    <a-space direction="vertical" v-if="selectedSucursal && selectEntrega == 'Sucursal'">
                        <p > Retiro en sucursal </p> 
                        <p>{{ selectedSucursal.nombre }} </p>
                    </a-space>
                </a-row>
            </a-card>
            <a-card class="mt-4">
                <a-row justify="space-between" style="width: 100%">
                    <h4>Fecha estimada de entrega</h4> 
                    {{ fechaEntrega == '1/1/1' ? '-' : fechaEntrega }}
                </a-row>
            </a-card>

            <a-row class="mt-6" justify="center" style="width: 100%;">
                <a-button block type="primary" size="large" @click="confirmarCompra" v-if="!moduloPago">
                    CONFIRMAR COMPRA
                </a-button>
            </a-row>
        </a-card>
    </a-row>

    <a-modal v-model:open="showModalEntrega" 
        title="¿Cómo quieres recibir tu compra?" 
        :confirm-loading="confirmLoading"
        @ok="selectMetodo">
        <a-divider type="horizontal" />
        <a-space direction="vertical" style="width: 100%; margin-bottom: 8px;">
            <a-radio-group v-model:value="selectEntrega" style="width: 100%;">
                <a-space direction="vertical" style="width: 100%; ">
                <a-card>
                    <a-radio  :value="'Domicilio'">Envío a Domicilio</a-radio>
                </a-card>
                
                <a-card class="mt-4">
                    <a-radio  :value="'Sucursal'">Retiro en sucursal</a-radio>
                    
                </a-card>
            </a-space>
            </a-radio-group>
        </a-space>
    </a-modal>

    <a-modal v-model:open="showModalDireccion" 
        title="¿Dónde quieres recibir tu compra?"
        @ok="selectDireccion">
        <a-divider type="horizontal" />
        <a-space direction="vertical" style="width: 100%; margin-bottom: 8px;">
            <a-radio-group v-model:value="selectDir" style="width: 100%;">
                <a-space direction="vertical" style="width: 100%; ">
                <a-card>
                    <a-radio :value="'actual'" :disabled="dirActual == null || dirActual == ''" >Mi dirección actual</a-radio>
                        
                        <p v-if="dirActual == null || dirActual == ''"> No se encontró una dirección </p>
                        <p v-else> {{ dirActual }} </p>
                    </a-card>
                
                <a-card class="mt-4">
                    <a-radio  :value="'nueva'">Otra dirección</a-radio>
                    <a-input  class="mt-4" v-if="selectDir == 'nueva'" v-model:value="dirNueva" placeholder="Ingrese una dirección" />
                </a-card>
            </a-space>
            </a-radio-group>
        </a-space>
    </a-modal>

    <a-modal v-model:open="showModalSucursales" 
        title="¿Dónde quieres retirar tu compra?"
        @ok="confirmSucursal">
        <a-divider type="horizontal" />
        <a-space direction="vertical" style="width: 100%; margin-bottom: 8px;">
            <mapaSelect @seleccionar-sucursal="seleccionarSucursal"></mapaSelect>
        </a-space>
    </a-modal>

</template>

<script>
import { message } from 'ant-design-vue';
import CarritoController from '../../services/CarritoController';
import { CloseOutlined, CheckOutlined, EditOutlined } from '@ant-design/icons-vue';
import mapaSelect from '../Sucursal/mapaSelect.vue';
import ApiEnvios from '../../services/ApiEnvios';
import PagosView from './PagosView.vue';

export default{
    data() {
        return {
            dataCarrito: {},
            productos: [],
            loading: true,
            enableCant: false,

            showModalEntrega: false,
            showModalDireccion: false,
            showModalSucursales: false,
            costoEnvio: 0,

            selectEntrega: '', //Domicilio o Sucursal
            selectDir: '', //actual o nueva 
            dirActual: '',
            dirNueva: '',

            selectedSucursal: null,
            fechaEntrega: '-',
            codSeguimiento: '',

            moduloPago: false
            
        }
    },
    beforeMount(){  
        this.getCarrito();
    },

    methods: {
        getCarrito(){
            CarritoController.buscarCarritoActual().then((response) => {
                if (response.status == 200){
                    this.dataCarrito = response.data;
                    this.productos = response.data.carritos;
                    if(this.dataCarrito.direccionDeEnvio != ''){
                        this.selectEntrega = 'Domicilio'
                        this.calcularEnvio();
                    }
                    if(this.dataCarrito.sucursalId != null){
                        this.selectEntrega = 'Sucursal'
                        this.selectedSucursal = this.dataCarrito.sucursalAsociada;
                    }
                    if(this.dataCarrito.fechaEstimadaEntrega){
                        const fecha = new Date(this.dataCarrito.fechaEstimadaEntrega);
                        const opcionesFormato = { year: 'numeric', month: 'numeric', day: 'numeric' };            
                        this.fechaEntrega = fecha.toLocaleDateString('es-ES', opcionesFormato);                       
                    }
                }else{
                    message.error('Error al obtener carrito');
                }
            })
        },
        quitarProducto(id){
            CarritoController.deleteProductoCarrito(id).then((response) => {
                if (response.status == 200){
                    message.success('Se quitó el producto de tu carrito.')
                    this.getCarrito();
                }else{
                    message.error('Error al quitar producto.');
                }
            })
        },

        guardarCant(item){
            CarritoController.editProductoCarrito(item).then((response) => {
                if (response.status == 200){
                    this.enableCant = false;
                    this.getCarrito();
                }else{
                    message.error('Error al modificar producto.');
                }
            })      
        },

        selectMetodo(){
            if(this.selectEntrega == ''){
                message.error('Seleccione una opción');
                return
            }
            if(this.selectEntrega == 'Domicilio'){
                this.dirActual = sessionStorage.getItem('direccion');                
                this.showModalDireccion = true;
            }else{
                this.showModalSucursales = true;
            }
            this.showModalEntrega = false;
        },

        selectDireccion(){
            if(this.selectDir == ''){
                message.error('Seleccione una opción');
                return
            }
            if(this.selectDir == 'actual'){
                this.dataCarrito.direccionDeEnvio = this.dirActual;
            }else{
                if(this.dirNueva == ''){
                    message.error('Ingrese una dirección');
                    return
                }
                this.dataCarrito.direccionDeEnvio = this.dirNueva;      
            }
            this.dataCarrito.sucursalId = null;

            this.calcularEnvio();
        },
        calcularEnvio(){
            ApiEnvios.calcularEnvio(this.dataCarrito.direccionDeEnvio).then((response) => {
                if(response.status == 200){
                    this.costoEnvio = response.data.shippingCost;
                    this.codSeguimiento = response.data.trackingNumber;
                    this.calcularFecha(response.data.deliveryTime);
                    this.showModalDireccion = false;
                }else{
                    message.error('Ocurrió un error al calcular el envío.')
                }
            })
        },
        seleccionarSucursal(sucursal){
            this.auxSucursal = sucursal;
        },

        confirmSucursal(){
            this.showModalSucursales = false;
            this.selectedSucursal = this.auxSucursal;
            this.dataCarrito.sucursalId = this.selectedSucursal.id;
            this.calcularFecha(this.selectedSucursal.tiempoEntrega);
            this.dataCarrito.direccionDeEnvio = '';
            this.codSeguimiento = '';
            this.costoEnvio = 0;
        },

        calcularFecha(dias){
            const fecha = new Date();
            fecha.setDate(fecha.getDate() + dias);
            this.dataCarrito.fechaEstimadaEntrega = fecha.toISOString();
            const opcionesFormato = { year: 'numeric', month: 'numeric', day: 'numeric' };            
            this.fechaEntrega = fecha.toLocaleDateString('es-ES', opcionesFormato);
        },
        confirmarCompra(){
            if(this.fechaEntrega == '1/1/1' || this.productos.length == 0){
                return
            }
            this.dataCarrito.total = this.totalProductos + this.costoEnvio;          
            this.dataCarrito.fecha = new Date().toISOString();
            
            CarritoController.actualizarOrden(this.dataCarrito).then((response) =>{
                if(response.status == 200){
                    this.moduloPago = true;
                }
                this.getCarrito();
            })
        }
    },
    computed: {
        totalProductos(){
            let total = 0;
            this.productos.forEach((item) => {
                total += item.pOs.precio * item.cantidad;
            })
            return total;
        }
    },

    components: { CloseOutlined, EditOutlined, CheckOutlined, mapaSelect, PagosView }
}

</script>